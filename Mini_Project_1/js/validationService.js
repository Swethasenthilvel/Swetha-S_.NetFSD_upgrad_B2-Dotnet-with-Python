// ===================== VALIDATION SERVICE =====================
const validationService = (() => {

  return {

    // -------- AUTH VALIDATION --------
    validateAuthForm: function (data, type) {
      let errors = {};

      if (!data.username || data.username.trim() === "") {
        errors.username = "Username required";
      }

      if (!data.password) {
        errors.password = "Password required";
      }

      if (type === "signup") {
        if (!data.confirm) {
          errors.confirm = "Confirm password required";
        } else if (data.password !== data.confirm) {
          errors.confirm = "Passwords do not match";
        }

        if (data.password && data.password.length < 6) {
          errors.password = "Minimum 6 characters";
        }
      }

      return errors;
    },

    // -------- EMPLOYEE VALIDATION --------
    validateEmployeeForm: function (emp) {
      let errors = {};

      const today = new Date();
      today.setHours(0, 0, 0, 0);

      // First Name
      if (!emp.firstName || emp.firstName.trim() === "") {
        errors.firstName = "First name required";
      }

      // Last Name
      if (!emp.lastName || emp.lastName.trim() === "") {
        errors.lastName = "Last name required";
      }

      // Email
      if (!emp.email || !/^\S+@\S+\.\S+$/.test(emp.email)) {
        errors.email = "Valid email required";
      }

      // Phone
      if (!emp.phone) {
        errors.phone = "Phone required";
      } else if (!/^\d{10}$/.test(emp.phone)) {
        errors.phone = "Phone must be 10 digits";
      }

      // Department
      if (!emp.department || emp.department.trim() === "") {
        errors.department = "Department required";
      }

      // Designation
      if (!emp.designation || emp.designation.trim() === "") {
        errors.designation = "Designation required";
      }

      // Salary
      if (!emp.salary || emp.salary <= 0) {
        errors.salary = "Salary must be greater than 0";
      }

      // Join Date
      if (!emp.joinDate) {
        errors.joinDate = "Join date required";
      } else {
        const selectedDate = new Date(emp.joinDate);
        selectedDate.setHours(0, 0, 0, 0);

        if (selectedDate > today) {
          errors.joinDate = "Future date is not allowed";
        }
      }

      // Status
      if (!emp.status || emp.status.trim() === "") {
        errors.status = "Status required";
      }

      return errors;
    }

  };

})();

// Export for Node / Jest
if (typeof module !== "undefined") {
  module.exports = validationService;
}