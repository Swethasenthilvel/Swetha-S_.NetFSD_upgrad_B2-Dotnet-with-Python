// ===================== STORAGE SERVICE =====================
const storageService = (() => {

  function getHeaders(withAuth = true) {
    const headers = {
      "Content-Type": "application/json"
    };

    if (withAuth && typeof authService !== "undefined" && authService.getToken) {
      const token = authService.getToken();
      if (token) {
        headers["Authorization"] = `Bearer ${token}`;
      }
    }

    return headers;
  }

  async function handleResponse(response) {
    let data = null;

    try {
      data = await response.json();
    } catch (error) {
      data = null;
    }

    if (!response.ok) {
      const err = new Error(data?.message || `HTTP error! Status: ${response.status}`);
      err.status = response.status;
      err.data = data;
      throw err;
    }

    return data;
  }

  function buildQuery(params = {}) {
    const query = new URLSearchParams();

    Object.keys(params).forEach(key => {
      const value = params[key];
      if (value !== undefined && value !== null && value !== "") {
        query.append(key, value);
      }
    });

    return query.toString() ? `?${query.toString()}` : "";
  }

  return {

    // ===================== AUTH =====================

    login: async function (credentials) {
      const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: "POST",
        headers: getHeaders(false),
        body: JSON.stringify(credentials)
      });

      return await handleResponse(response);
    },

    register: async function (userData) {
      const response = await fetch(`${API_BASE_URL}/auth/register`, {
        method: "POST",
        headers: getHeaders(false),
        body: JSON.stringify(userData)
      });

      return await handleResponse(response);
    },

    // ===================== EMPLOYEES =====================

    getAll: async function (params = {}) {
      const queryString = buildQuery(params);

      const response = await fetch(`${API_BASE_URL}/employees${queryString}`, {
        method: "GET",
        headers: getHeaders(true)
      });

      return await handleResponse(response);
    },

    getById: async function (id) {
      const response = await fetch(`${API_BASE_URL}/employees/${id}`, {
        method: "GET",
        headers: getHeaders(true)
      });

      return await handleResponse(response);
    },

    add: async function (employee) {
      const response = await fetch(`${API_BASE_URL}/employees`, {
        method: "POST",
        headers: getHeaders(true),
        body: JSON.stringify(employee)
      });

      return await handleResponse(response);
    },

    update: async function (id, employee) {
      const response = await fetch(`${API_BASE_URL}/employees/${id}`, {
        method: "PUT",
        headers: getHeaders(true),
        body: JSON.stringify(employee)
      });

      return await handleResponse(response);
    },

    remove: async function (id) {
      const response = await fetch(`${API_BASE_URL}/employees/${id}`, {
        method: "DELETE",
        headers: getHeaders(true)
      });

      return await handleResponse(response);
    },

    getDashboard: async function () {
      const response = await fetch(`${API_BASE_URL}/employees/dashboard`, {
        method: "GET",
        headers: getHeaders(true)
      });

      return await handleResponse(response);
    }
  };

})();

// Export for Node / Jest
if (typeof module !== "undefined") {
  module.exports = storageService;
}