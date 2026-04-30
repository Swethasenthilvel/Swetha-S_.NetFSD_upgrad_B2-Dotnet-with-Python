from functools import reduce


# -------------------------------
# FILTER: Get only critical incidents
# -------------------------------
def get_critical_incidents(incidents):
    return list(filter(lambda i: i.severity == "critical", incidents))


# -------------------------------
# MAP: Convert incidents to dict payloads
# -------------------------------
def build_jira_payloads(incidents):
    return list(map(lambda i: i.to_dict(), incidents))


# -------------------------------
# REDUCE: Count incidents by team
# -------------------------------
def count_by_team(incidents):
    return reduce(
        lambda acc, i: {
            **acc,
            i.assigned_team: acc.get(i.assigned_team, 0) + 1
        },
        incidents,
        {}
    )