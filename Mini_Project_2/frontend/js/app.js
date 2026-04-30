// ===================== APP.JS =====================

let currentSearch = "";
let currentDept = "";
let currentStatus = "";
let currentSortField = "id";
let currentSortDir = "asc";
let currentPage = 1;
const pageSize = 10;

let editId = null;
let deleteId = null;

$(document).ready(() => {

  // ---------------- LOGIN CHECK ----------------
  if (authService.isLoggedIn()) {
    initApp();
  } else {
    uiService.show("#loginView");
  }

  // ---------------- NAV ACTIVE STATE ----------------
  function setActiveNav(buttonId) {
    $("#navDashboard, #navEmployees").removeClass("active");
    $(buttonId).addClass("active");
  }

  // ---------------- FILTER ACTIVE STATE ----------------
  function updateStatusButtons(activeId) {
    $("#filterAll, #filterActive, #filterInactive")
      .removeClass("active btn-primary")
      .addClass("btn-outline-primary");

    $(activeId)
      .removeClass("btn-outline-primary")
      .addClass("active btn-primary");
  }

  // ---------------- LOGIN ----------------
  $("#loginBtn").click(async () => {
    const username = $("#loginUsername").val().trim();
    const password = $("#loginPassword").val();

    $("#errLoginUser, #errLoginPass").text("");

    if (!username) {
      $("#errLoginUser").text("Username required");
      return;
    }

    if (!password) {
      $("#errLoginPass").text("Password required");
      return;
    }

    try {
      const success = await authService.login(username, password);

      if (success) {
        uiService.showToast("Login successful");
        $("#loginUsername, #loginPassword").val("");
        initApp();
      } else {
        $("#errLoginPass").text("Invalid credentials");
      }
    } catch (err) {
      $("#errLoginPass").text(err.message || "Login failed");
    }
  });


  // ---------------- SIGNUP ----------------
  $("#signupBtn").click(async (e) => {
    e.preventDefault();

    const username = $("#sUser").val().trim();
    const password = $("#sPass").val();
    const confirm = $("#sConfirm").val();

    $("#errUser, #errPass, #errConfirm").text("").addClass("text-danger");
$("#signupMsg").text("").removeClass("text-danger text-success");

    const errors = validationService.validateAuthForm(
      { username, password, confirm },
      "signup"
    );

    if (Object.keys(errors).length) {
  $("#errUser").text(errors.username || "").addClass("text-danger");
  $("#errPass").text(errors.password || "").addClass("text-danger");
  $("#errConfirm").text(errors.confirm || "").addClass("text-danger");
  return;
}
    try {
      const success = await authService.signup(username, password);

      if (!success) {
        $("#signupMsg").text("Username already exists").addClass("text-danger");
        return;
      }

      $("#signupMsg").text("Account created successfully").addClass("text-success");
      $("#sUser, #sPass, #sConfirm").val("");

      setTimeout(() => {
        uiService.show("#loginView");
      }, 1500);

    } catch (err) {
      $("#signupMsg").text(err.message || "Signup failed").addClass("text-danger");
    }
  });

// Password toggle
$("#togglePassword").click(function () {
  const input = $("#loginPassword");
  const icon = $(this).find("i");

  if (input.attr("type") === "password") {
    input.attr("type", "text");
    icon.removeClass("bi-eye-slash").addClass("bi-eye");
  } else {
    input.attr("type", "password");
    icon.removeClass("bi-eye").addClass("bi-eye-slash");
  }
});

  // ---------------- NAV BETWEEN LOGIN & SIGNUP ----------------
  $("#goSignup").click((e) => {
    e.preventDefault();
    uiService.show("#signupView");
  });

  $("#goLogin").click((e) => {
    e.preventDefault();
    uiService.show("#loginView");
  });

  // ---------------- LOGOUT ----------------
  $("#logoutBtn").click(() => {
    authService.logout();

    $("#userInfo").text("");
    $("#roleBadge").text("");
    $("#loginUsername, #loginPassword").val("");

    uiService.show("#loginView");
  });

  // ---------------- NAVIGATION ----------------
  $("#navDashboard").click(() => {
    uiService.show("#dashboardView");
    setActiveNav("#navDashboard");
    renderDashboard();
  });

  $("#navEmployees").click(() => {
    uiService.show("#employeeView");
    setActiveNav("#navEmployees");
    renderEmployees();
  });

  // ---------------- SEARCH & FILTER ----------------
  let searchTimer;

  $("#searchInput").on("input", function () {
    clearTimeout(searchTimer);
    currentSearch = $(this).val().trim();
    currentPage = 1;

    searchTimer = setTimeout(() => {
      renderEmployees();
    }, 350);
  });

  $("#deptFilter").change(function () {
    currentDept = $(this).val();
    currentPage = 1;
    renderEmployees();
  });

  $("#filterAll").click(() => {
    currentStatus = "";
    currentPage = 1;
    updateStatusButtons("#filterAll");
    renderEmployees();
  });

  $("#filterActive").click(() => {
    currentStatus = "Active";
    currentPage = 1;
    updateStatusButtons("#filterActive");
    renderEmployees();
  });

  $("#filterInactive").click(() => {
    currentStatus = "Inactive";
    currentPage = 1;
    updateStatusButtons("#filterInactive");
    renderEmployees();
  });

  // ---------------- ADD EMPLOYEE ----------------
  $("#addEmpBtnNav, #addEmpTopBtn, #addEmpBtn").click(() => {
    editId = null;
    uiService.clearForm();
    new bootstrap.Modal(document.getElementById("empModal")).show();
  });

  // ---------------- SAVE EMPLOYEE ----------------
  $("#saveEmp").click(async () => {
    const emp = {
      firstName: $("#firstName").val().trim(),
      lastName: $("#lastName").val().trim(),
      email: $("#email").val().trim(),
      phone: $("#phone").val().trim(),
      department: $("#dept").val(),
      designation: $("#designation").val().trim(),
      salary: Number($("#salary").val()) || 0,
      joinDate: $("#joinDate").val(),
      status: $("#status").val()
    };

    const errors = validationService.validateEmployeeForm(emp);
    uiService.showInlineErrors(errors);

    if (Object.keys(errors).length) return;

    try {
      if (editId) {
          uiService.showToast("Employee updated successfully");
        await employeeService.update(editId, emp);
        editId = null;
      } else {
        await employeeService.add(emp);
        uiService.showToast("Employee added successfully");
      }

      await renderEmployees();
      await renderDashboard();

      const empModalEl = document.getElementById("empModal");
      const empModal =
        bootstrap.Modal.getInstance(empModalEl) || new bootstrap.Modal(empModalEl);
      empModal.hide();

    } catch (err) {
      uiService.showToast(err.message || "Failed to save employee");
    }
  });

  // ---------------- VIEW EMPLOYEE ----------------
  $(document).on("click", ".viewBtn", async function () {
    const id = $(this).data("id");

    try {
      const emp = await employeeService.getById(id);
      uiService.showViewModal(emp);
    } catch (err) {
      uiService.showToast(err.message || "Failed to fetch employee details");
    }
  });

  // ---------------- EDIT EMPLOYEE ----------------
  $(document).on("click", ".editBtn", async function () {
    const id = $(this).data("id");
    editId = id;

    try {
      const emp = await employeeService.getById(id);
      uiService.populateForm(emp);
      new bootstrap.Modal(document.getElementById("empModal")).show();
    } catch (err) {
      uiService.showToast(err.message || "Failed to fetch employee details");
    }
  });

  // ---------------- DELETE EMPLOYEE ----------------
  $(document).on("click", ".deleteBtn", async function () {
    const id = $(this).data("id");
    deleteId = id;

    try {
      const emp = await employeeService.getById(id);
      uiService.showDeleteModal(emp);
    } catch (err) {
      uiService.showToast(err.message || "Failed to fetch employee details");
    }
  });

  $("#confirmDelete").click(async () => {
    if (!deleteId) return;

    try {
      await employeeService.remove(deleteId);
      uiService.showToast("Employee deleted successfully");
      deleteId = null;

      await renderEmployees();
      await renderDashboard();

      const delModalEl = document.getElementById("deleteModal");
      const delModal =
        bootstrap.Modal.getInstance(delModalEl) || new bootstrap.Modal(delModalEl);
      delModal.hide();

    } catch (err) {
      uiService.showToast(err.message || "Failed to delete employee");
    }
  });

  // ---------------- PAGINATION ----------------
  $(document).on("click", ".page-link[data-page]", function (e) {
    e.preventDefault();

    const page = Number($(this).data("page"));

    if (!isNaN(page) && page > 0) {
      currentPage = page;
      renderEmployees();
    }
  });

  // ---------------- TABLE HEADER SORTING ----------------
  $(document).on("click", "th.sortable", function () {
    const field = $(this).data("field");
    let dir = $(this).data("dir") || "asc";

    dir = dir === "asc" ? "desc" : "asc";
    $(this).data("dir", dir);

    currentSortField = field;
    currentSortDir = dir;
    currentPage = 1;

    renderEmployees();

    $("th.sortable .sort-arrow").text("▲");
    $(this).find(".sort-arrow").text(dir === "asc" ? "▲" : "▼");
  });

  // expose helpers inside ready scope usage
  window.__setActiveNav = setActiveNav;
  window.__updateStatusButtons = updateStatusButtons;
});

// ===================== INIT APP =====================
function initApp() {
  uiService.show("#dashboardView");

  $("#userInfo").text(authService.getCurrentUser());
  $("#roleBadge").text(authService.getRole());

  if (typeof window.__setActiveNav === "function") {
    window.__setActiveNav("#navDashboard");
  }

  if (typeof window.__updateStatusButtons === "function") {
    window.__updateStatusButtons("#filterAll");
  }

  currentStatus = "";

  if (!authService.isAdmin()) {
    $("#viewerNotice").removeClass("d-none");
    $("#addEmpBtnNav, #addEmpTopBtn, #addEmpBtn").hide();
  } else {
    $("#viewerNotice").addClass("d-none");
    $("#addEmpBtnNav, #addEmpTopBtn, #addEmpBtn").show();
  }

  renderDashboard();
  renderEmployees();
}
// ===================== RENDER EMPLOYEES =====================
async function renderEmployees() {
  try {
    const params = {
      search: currentSearch,
      department: currentDept,
      status: currentStatus,
      sortBy: currentSortField,
      sortDir: currentSortDir,
      page: currentPage,
      pageSize: pageSize
    };

    const result = await employeeService.getAll(params);

    uiService.renderEmployeeTable(result);

    if (uiService.renderPagination) {
      uiService.renderPagination(result);
    }

    if (!authService.isAdmin()) {
      $(".editBtn, .deleteBtn").hide();
    }

  } catch (err) {
    uiService.showToast(err.message || "Failed to load employees");
  }
}

// ===================== RENDER DASHBOARD =====================
async function renderDashboard() {
  try {
    const data = await dashboardService.getSummary();

    uiService.renderDashboard(
      {
        total: data.totalEmployees,
        active: data.activeCount,
        inactive: data.inactiveCount,
        departments: data.totalDepartments
      },
      data.departmentBreakdown,
      data.recentEmployees
    );

  } catch (err) {
    uiService.showToast(err.message || "Failed to load dashboard");
  }
}