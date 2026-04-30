import json
from collections import Counter
from datetime import datetime
import os


class ReportGenerator:

    def __init__(self, incidents):
        self.incidents = incidents

    # -------------------------------
    # EXPORT JSON
    # -------------------------------
    def export_json(self):
        os.makedirs("output", exist_ok=True)

        data = [i.to_dict() for i in self.incidents]

        with open("output/report.json", "w", encoding="utf-8") as f:
            json.dump(data, f, indent=4)

    # -------------------------------
    # GENERATE HTML
    # -------------------------------
    def generate_html(self):
        os.makedirs("output", exist_ok=True)

        total = len(self.incidents)

        severity_count = Counter(i.severity for i in self.incidents)
        type_count = Counter(getattr(i, "type", "unknown") for i in self.incidents)
        team_count = Counter(i.assigned_team for i in self.incidents)

        now = datetime.now().strftime("%Y-%m-%d %H:%M")

        # -------------------------------------CSS-----------------------------------------
        style = """
<style>
* {
    box-sizing: border-box;
}

html, body {
    margin: 0;
    padding: 0;
    width: 100%;
    height: 100%;
}

/* BODY */
body {
    font-family: Arial, sans-serif;
    background: #eef1f5;
    font-size: 13px;
    overflow: hidden; 
}

/* HEADER */
.header {
    background: #1f3c88;
    color: white;
    padding: 10px 15px;
    font-size: 16px;
    width: 100%;
    height: 50px;
}

/* CONTAINER */
.container {
    padding: 8px 12px;
    width: 100%;
    height: calc(100vh - 50px);
    display: flex;
    flex-direction: column;
    overflow: hidden;
}

/* CARDS */
.card-box {
    display: flex;
    gap: 10px;
    margin-bottom: 10px;
    flex-wrap: wrap;
}

.card {
    background: white;
    padding: 8px;
    border-radius: 6px;
    text-align: center;
    flex: 1;
    min-width: 120px;
    font-size: 13px;
}

/* BADGE */
.badge {
    padding: 3px 6px;
    border-radius: 8px;
    font-size: 11px;
    margin-right: 5px;
    display: inline-block;
}

/* COLORS */
.critical { background: #ffcccc; color: red; }
.high { background: #ffe0b3; color: #cc7a00; }
.medium { background: #cce5ff; color: #004085; }
.low { background: #d4edda; color: #155724; }

/* SECTION */
.section {
    margin-bottom: 6px;
    font-size: 13px;
}

.section b {
    display: inline-block;
    width: 70px;
}

/* TABLE WRAPPER */
.table-wrapper {
    width: 100%;
    flex: 1;
    overflow: auto; 
    border-radius: 6px;
}

/* TABLE */
table {
    width: 100%;
    border-collapse: collapse;
    background: white;
    table-layout: auto;
}

/* TABLE CELLS */
th, td {
    padding: 9px;
    border: 1px solid #ddd;
    text-align: center;
    font-size: 11px;
}

/* HEADER */
th {
    background: #1f3c88;
    color: white;
    position: sticky;
    top: 0; 
    z-index: 2;
}

/* TITLE COLUMN */
td:nth-child(2), th:nth-child(2) {
    text-align: left;
    max-width: 250px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

td:nth-child(3), th:nth-child(3) {
    width: 160px;   
}

/* LEFT ALIGN */
td:nth-child(3),
td:nth-child(4),
td:nth-child(5),
th:nth-child(3),
th:nth-child(4),
th:nth-child(5) {
    text-align: left;
}

/* HOVER */
tr:hover {
    background-color: #f5f5f5;
}

/* FOOTER */
footer {
    text-align: center;
    padding: 6px;
    font-size: 11px;
    color: gray;
}
</style>
"""

        # ------------------------------------HTML-------------------------------
        html = f"""<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Incident Report</title>
{style}
</head>

<body>

<div class="header">
    IT Incident Auto-Triage Report |
    Generated: {now} |
    Total: {total}
</div>

<div class="container">

<div class="card-box">
    <div class="card">{total}<br>Total</div>
    <div class="card critical">{severity_count.get("critical", 0)}<br>Critical</div>
    <div class="card high">{severity_count.get("high", 0)}<br>High</div>
    <div class="card medium">{severity_count.get("medium", 0)}<br>Medium</div>
    <div class="card low">{severity_count.get("low", 0)}<br>Low</div>
</div>

<div class="section"><b>Type:</b>
"""

        for t, c in type_count.items():
            html += f'<span class="badge">{t}:{c}</span> '

        html += "</div><div class='section'><b>Severity:</b> "

        for s, c in severity_count.items():
            html += f'<span class="badge {s}">{s}:{c}</span> '

        html += "</div><div class='section'><b>Team:</b> "

        for t, c in team_count.items():
            html += f'<span class="badge">{t}:{c}</span> '

        html += "</div>"

        html += """
<div class="table-wrapper">
<table>
<tr>
<th>ID</th>
<th>Title</th>
<th>Severity</th>
<th>Type</th>
<th>Team</th>
<th>Time</th>
<th>SNOW</th>
<th>JIRA</th>
<th>AZURE</th>
</tr>
"""

        for i in self.incidents:
            html += f"""
<tr>
<td>{i.id}</td>
<td title="{i.title}">{i.title}</td>
<td><span class="badge {i.severity}">{i.severity.upper()}</span></td>
<td>{getattr(i, "type", "unknown")}</td>
<td>{i.assigned_team}</td>
<td>{i.timestamp.strftime("%m-%d %H:%M")}</td>
<td>{i.ticket_ids.get("snow", "-")}</td>
<td>{i.ticket_ids.get("jira", "-")}</td>
<td>{i.ticket_ids.get("azure", "-")}</td>
</tr>
"""

        html += """
</table>
</div>

<footer>
    Generated by Swetha | IT Incident Tracker
</footer>

</div>
</body>
</html>
"""

        with open("output/report.html", "w", encoding="utf-8") as f:
            f.write(html)