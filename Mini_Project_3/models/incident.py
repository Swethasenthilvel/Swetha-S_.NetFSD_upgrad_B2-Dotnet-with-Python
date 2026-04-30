from datetime import datetime
from utils.classifier import detect_type, detect_severity

SEVERITY_ORDER = {"critical": 0, "high": 1, "medium": 2, "low": 3}


class Incident:
    def __init__(self, id, title, description, reported_by, timestamp, assigned_team):
        self.id = id
        self.title = title
        self.description = description
        self.reported_by = reported_by
        self.timestamp = datetime.fromisoformat(timestamp.replace("Z", "+00:00"))
        self.assigned_team = assigned_team
        self._severity = None
        self.ticket_ids = {}

    def classify(self):
        raise NotImplementedError("Subclasses must implement classify()")

    @property
    def severity(self):
        return self._severity

    #  Safe sorting
    def __lt__(self, other):
        return SEVERITY_ORDER.get(self.severity, 99) < SEVERITY_ORDER.get(other.severity, 99)

    #  Dictionary conversion
    def to_dict(self):
        return {
            "id": self.id,
            "title": self.title,
            "type": getattr(self, "type", "unknown"),
            "severity": self.severity,
            "assigned_team": self.assigned_team,
            "tickets": self.ticket_ids
        }

    def __str__(self):
        return f"{self.id} - {self.title} ({self.severity})"

    def __repr__(self):
        return f"Incident(id={self.id}, severity={self.severity})"

    # JSON VALIDATION
    @staticmethod
    def validate_json(data):
        required_fields = {
            "id",
            "title",
            "description",
            "reported_by",
            "timestamp",
            "assigned_team"
        }

        for record in data:
            if not required_fields.issubset(record.keys()):
                raise ValueError(f"Invalid JSON record: {record}")


# -------------------------------
# SUBCLASSES
# -------------------------------

class NetworkIncident(Incident):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.affected_host = None
        self.protocol = None

    def classify(self):
        text = self.title + " " + self.description
        self._severity = detect_severity(text)

    def escalate(self):
        return f"Escalated to network on-call for {self.id}"


class AppIncident(Incident):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.app_name = None
        self.error_code = None

    def classify(self):
        text = self.title + " " + self.description
        self._severity = detect_severity(text)

    def get_stack_trace(self):
        return f"Stack trace snippet for {self.id}"


class SecurityIncident(Incident):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.threat_type = None
        self.source_ip = None

    def classify(self):
        text = self.title + " " + self.description
        self._severity = detect_severity(text)

    def notify_soc(self):
        return f"SOC notified for {self.id}"


# -------------------------------
# FACTORY FUNCTION (FIXED)
# -------------------------------

def create_incident(data):
    text = data["title"] + " " + data["description"]
    type_ = detect_type(text)

    mapping = {
        "network": NetworkIncident,
        "app": AppIncident,
        "security": SecurityIncident
    }

    #  FIX: fallback to AppIncident
    cls = mapping.get(type_, AppIncident)

    obj = cls(**data)

    #  store type for report
    obj.type = type_

    obj.classify()
    return obj


# -------------------------------
# INCIDENT ITERATOR
# -------------------------------

class IncidentIterator:
    def __init__(self, incidents, severity=None):
        self.original = incidents
        self.severity = severity
        self.reset()

    def reset(self):
        if self.severity:
            self.filtered = [i for i in self.original if i.severity == self.severity]
        else:
            self.filtered = self.original
        self.index = 0

    def __iter__(self):
        self.reset()
        return self

    def __next__(self):
        if self.index >= len(self.filtered):
            raise StopIteration
        item = self.filtered[self.index]
        self.index += 1
        return item


# -------------------------------
# BATCH GENERATOR
# -------------------------------

def batch_incidents(incidents, batch_size=3):
    """Yield incidents in batches of given size."""
    for i in range(0, len(incidents), batch_size):
        yield incidents[i: i + batch_size]