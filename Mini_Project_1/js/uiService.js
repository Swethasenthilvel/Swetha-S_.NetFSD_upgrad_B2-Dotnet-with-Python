// ===================== UI SERVICE =====================
const uiService = (() => {

  function getDeptColor(dept) {
    switch (dept) {
      case "Engineering": return "bg-primary";
      case "Marketing": return "bg-warning text-dark";
      case "HR": return "bg-info text-dark";
      case "Finance": return "bg-success";
      case "Operations": return "bg-secondary";
      default: return "bg-dark";
    }
  }

  function formatDate(dateValue) {
    if (!dateValue) return "";
    const d = new Date(dateValue);
    if (isNaN(d.getTime())) return dateValue;
    return d.toLocaleDateString();
  }

  return {

    show: (id) => {
      $("#loginView,#signupView,#dashboardView,#employeeView").addClass("d-none");
      $(id).removeClass("d-none");

      if (id === "#loginView" || id === "#signupView") {
        $("#mainNavbar").addClass("d-none");
        $("body").addClass("auth-bg").removeClass("dashboard-bg");
      } else {
        $("#mainNavbar").removeClass("d-none");
        $("body").addClass("dashboard-bg").removeClass("auth-bg");
        uiService.applyRoleUI();
      }
    },

    applyRoleUI: function () {
      const isAdmin = authService.isAdmin();
      const role = authService.getRole() || "";

      $("#roleBadge").text(role || "User");

      if (isAdmin) {
        $("#addEmpBtn,#addEmpTopBtn").removeClass("d-none");
        $("#viewerNotice").addClass("d-none");
        $(".editBtn,.deleteBtn").removeClass("d-none");
      } else {
        $("#addEmpBtn,#addEmpTopBtn").addClass("d-none");
        $("#viewerNotice").removeClass("d-none");
        $(".editBtn,.deleteBtn").addClass("d-none");
      }
    },

    renderDashboard: function (summary, deptData, recent) {
      $("#totalEmp").text(summary.total);
      $("#activeEmp").text(summary.active);
      $("#inactiveEmp").text(summary.inactive);
      $("#deptCount").text(summary.departments);

      let total = summary.total || 0;
      let deptHTML = "";

      (deptData || []).forEach((item, index) => {
        const percent = item.percentage ?? (total ? Math.round((item.count / total) * 100) : 0);
        const colors = ["bg-primary", "bg-warning", "bg-info", "bg-success", "bg-secondary"];
        const color = colors[index % colors.length];

        deptHTML += `
          <tr>
            <td><span class="badge ${color}">${item.department}</span></td>
            <td>${item.count}</td>
            <td>
              <div class="progress">
                <div class="progress-bar ${color}" style="width:${percent}%"></div>
              </div>
            </td>
            <td><b>${percent}%</b></td>
          </tr>`;
      });

      $("#deptTable").html(deptHTML);

      let recentHTML = "";

      (recent || []).forEach(e => {
        const initials = `${e.firstName?.[0] || ""}${e.lastName?.[0] || ""}`;
        recentHTML += `
          <div class="emp-row d-flex justify-content-between align-items-center">
            <div class="d-flex align-items-center gap-3">
              <div class="avatar">${initials}</div>
              <div>
                <div class="fw-bold">${e.firstName} ${e.lastName}</div>
                <small class="text-muted">${e.designation}</small>
              </div>
            </div>
            <div class="emp-badges">
              <span class="badge ${getDeptColor(e.department)}">${e.department}</span>
              <span class="badge ${e.status === 'Active' ? 'bg-success' : 'bg-danger'}">${e.status}</span>
            </div>
          </div>`;
      });

      $("#recentList").html(recentHTML);
    },

    renderEmployeeTable: function (result) {
      const data = result?.data || [];
      let html = "";

      if (data.length === 0) {
        html = `<tr><td colspan="10" class="text-center text-muted">No employees found</td></tr>`;
      } else {
        data.forEach(e => {
          html += `
            <tr>
              <td>#${e.id}</td>
              <td><div class="avatar">${e.firstName?.[0] || ""}${e.lastName?.[0] || ""}</div></td>
              <td>${e.firstName} ${e.lastName}</td>
              <td>${e.email}</td>
              <td><span class="badge ${getDeptColor(e.department)}">${e.department}</span></td>
              <td>${e.designation}</td>
              <td>₹${Number(e.salary || 0).toLocaleString()}</td>
              <td>${formatDate(e.joinDate)}</td>
              <td><span class="badge ${e.status === 'Active' ? 'bg-success' : 'bg-danger'}">${e.status}</span></td>
              <td>
                <button class="btn btn-sm btn-light border me-1 viewBtn" data-id="${e.id}">
                  <i class="bi bi-eye"></i>
                </button>
                <button class="btn btn-sm btn-light border me-1 editBtn ${authService.isAdmin() ? "" : "d-none"}" data-id="${e.id}">
                  <i class="bi bi-pencil"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger deleteBtn ${authService.isAdmin() ? "" : "d-none"}" data-id="${e.id}">
                  <i class="bi bi-trash"></i>
                </button>
              </td>
            </tr>`;
        });
      }

      $("#employeeTable").html(html);

      const totalCount = result?.totalCount || 0;
      const page = result?.page || 1;
      const pageSize = result?.pageSize || data.length || 1;

      const start = totalCount === 0 ? 0 : ((page - 1) * pageSize) + 1;
      const end = totalCount === 0 ? 0 : Math.min(page * pageSize, totalCount);

      $("#empCount").text(`Showing ${start}-${end} of ${totalCount} employees`);

      this.applyRoleUI();
    },

    renderPagination: function (result) {
      if (!result) {
        $("#pagination").html("");
        return;
      }

      const page = result.page || 1;
      const totalPages = result.totalPages || 1;

      if (totalPages <= 1) {
        $("#pagination").html("");
        return;
      }

      let html = `<li class="page-item ${result.hasPrevPage ? "" : "disabled"}">
        <a class="page-link" href="#" data-page="${page - 1}">Prev</a>
      </li>`;

      for (let i = 1; i <= totalPages; i++) {
        html += `
          <li class="page-item ${i === page ? "active" : ""}">
            <a class="page-link" href="#" data-page="${i}">${i}</a>
          </li>`;
      }

      html += `<li class="page-item ${result.hasNextPage ? "" : "disabled"}">
        <a class="page-link" href="#" data-page="${page + 1}">Next</a>
      </li>`;

      $("#pagination").html(html);
    },

    showViewModal: function (emp) {
      $("#viewAvatar").text(`${emp.firstName?.[0] || ""}${emp.lastName?.[0] || ""}`);
      $("#viewName").text(`${emp.firstName} ${emp.lastName}`);
      $("#viewDept").text(emp.department);
      $("#viewEmail").text(emp.email);
      $("#viewPhone").text(emp.phone);
      $("#viewDesig").text(emp.designation);
      $("#viewSalary").text("₹" + Number(emp.salary || 0).toLocaleString());
      $("#viewDate").text(formatDate(emp.joinDate));
      $("#viewStatus").html(
        `<span class="badge ${emp.status === 'Active' ? 'bg-success' : 'bg-danger'}">${emp.status}</span>`
      );
      new bootstrap.Modal(document.getElementById("viewEmpModal")).show();
    },

    showDeleteModal: function (emp) {
      $("#deleteText").text(`Are you sure you want to delete ${emp.firstName} ${emp.lastName}?`);
      new bootstrap.Modal(document.getElementById("deleteModal")).show();
    },

   populateForm: function (emp) {
  $("#firstName").val(emp.firstName);
  $("#lastName").val(emp.lastName);
  $("#email").val(emp.email);
  $("#phone").val(emp.phone);
  $("#dept").val(emp.department);
  $("#designation").val(emp.designation);
  $("#salary").val(emp.salary);
  $("#joinDate").val(emp.joinDate ? emp.joinDate.split("T")[0] : "");
  $("#status").val(emp.status);

  $("#empModal .modal-title").text("Edit Employee");
  $("#saveEmp").text("Update Employee");
},

   clearForm: function () {
  $("#firstName,#lastName,#email,#phone,#dept,#designation,#salary,#joinDate,#status").val("");
  $("#empModal .modal-title").text("Add Employee");
  $("#saveEmp").text("Add Employee");
  $("#errF,#errL,#errE,#errP,#errD,#errDes,#errS,#errJ,#errSt").text("");
},

    showToast: function (msg, event) {
      if (event && event.defaultPrevented) return;

      const toastEl = document.getElementById("toast");
      if (!toastEl) return;

      $("#toastMsg").text(msg);
      new bootstrap.Toast(toastEl).show();
    },

    showInlineErrors: function (errors) {
  $("#errF").text(errors.firstName || "");
  $("#errL").text(errors.lastName || "");
  $("#errE").text(errors.email || "");
  $("#errP").text(errors.phone || "");
  $("#errD").text(errors.department || "");
  $("#errDes").text(errors.designation || "");
  $("#errS").text(errors.salary || "");
  $("#errJ").text(errors.joinDate || "");
  $("#errSt").text(errors.status || "");
}

  };

})();