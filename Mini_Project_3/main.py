import argparse
import json

from models.incident import create_incident, Incident, batch_incidents
from services.jira import create_jira_ticket
from services.servicenow import create_snow_ticket
from services.azure_boards import create_azure_ticket
from models.report import ReportGenerator
from utils.helpers import get_critical_incidents, count_by_team


def main():
    parser = argparse.ArgumentParser(
        description="IT Incident Auto-Triage Tool"
    )
    parser.add_argument(
        "--severity",
        help="Filter by severity (critical/high/medium/low)"
    )
    args = parser.parse_args()

    # LOAD DATA
    with open("data/incidents.json", encoding="utf-8") as f:
        data = json.load(f)

    # VALIDATE
    Incident.validate_json(data)

    # CREATE INCIDENTS
    incidents = [create_incident(i) for i in data]

    # FILTER
    if args.severity:
        severity = args.severity.lower()
        valid = {"critical", "high", "medium", "low"}

        if severity not in valid:
            raise ValueError("Invalid severity")

        incidents = [i for i in incidents if i.severity == severity]

    # GENERATOR EXPRESSION (bonus)
    critical_generator = (i for i in incidents if i.severity == "critical")

    # Just consuming generator (demo usage)
    for _ in critical_generator:
        pass

    # CREATE TICKETS (BATCH)
    for batch in batch_incidents(incidents, 3):
        for inc in batch:
            inc.ticket_ids["jira"] = create_jira_ticket(inc)
            inc.ticket_ids["snow"] = create_snow_ticket(inc)
            inc.ticket_ids["azure"] = create_azure_ticket(inc)

    # SORT
    incidents.sort()

    #  USE REDUCE FUNCTION
    team_stats = count_by_team(incidents)
    print("Team Distribution:", team_stats)

    # REPORT
    report = ReportGenerator(incidents)
    report.generate_html()
    report.export_json()

    print("Execution completed successfully")


if __name__ == "__main__":
    main()