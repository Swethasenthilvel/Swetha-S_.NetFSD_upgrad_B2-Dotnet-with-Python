import re

# -------------------------------
# TYPE PATTERNS
# -------------------------------
NETWORK = re.compile(
    r"(tcp|udp|icmp|vlan|switch|firewall|\b\d{1,3}(?:\.\d{1,3}){3}\b|dns|packet)",
    re.IGNORECASE
)

SECURITY = re.compile(
    r"(breach|ransomware|phishing|malware|unauthorized|brute[-\s]?force|suspicious|attack)",
    re.IGNORECASE
)

APP = re.compile(
    r"(exception|error|http[-\s]?\d+|stack trace|api|service|failure)",
    re.IGNORECASE
)

# -------------------------------
# SEVERITY PATTERNS
# -------------------------------
CRITICAL = re.compile(
    r"(outage|down|breach|ransomware)",
    re.IGNORECASE
)

HIGH = re.compile(
    r"(timeout|failing|unavailable|unreachable|http[-\s]?5\d{2}|exception|error|suspicious|unauthorized login)",
    re.IGNORECASE
)

MEDIUM = re.compile(
    r"(phishing|slow|degraded|warning|intermittent|latency|packet loss)",
    re.IGNORECASE
)

LOW = re.compile(
    r"(minor|non[-\s]?production|low impact|misconfiguration)",
    re.IGNORECASE
)

# -------------------------------
# DETECT TYPE
# -------------------------------
def detect_type(text: str) -> str:
    """
    Returns: network / security / app
    """

    if SECURITY.search(text):
        return "security"
    elif NETWORK.search(text):
        return "network"
    elif APP.search(text):
        return "app"

    return "general"


# -------------------------------
# DETECT SEVERITY
# -------------------------------
def detect_severity(text: str) -> str:
    """
    Returns: critical / high / medium / low
    Order is IMPORTANT to avoid wrong classification
    """

    # 1. Critical (highest priority)
    if CRITICAL.search(text):
        return "critical"
    #2.High 
    if HIGH.search(text):
        return "high"
    # 3.Medium FIRST (to protect phishing, intermittent, etc.)
    if MEDIUM.search(text):
        return "medium"
    # 4. Low
    if LOW.search(text):
        return "low"

    return "low"