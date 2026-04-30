const authService = (() => {
  let currentUser = null;
  let currentRole = null;
  let token = null;

  return {

    signup: async (username, password, role = "Viewer") => {
      const result = await storageService.register({ username, password, role });
      return result.success;
    },

    login: async (username, password) => {
      const result = await storageService.login({ username, password });

      if (!result.success) return false;

      currentUser = result.username;
      currentRole = result.role;
      token = result.token;

      return true;
    },

    logout: () => {
      currentUser = null;
      currentRole = null;
      token = null;
    },

    isLoggedIn: () => token !== null,
    getCurrentUser: () => currentUser,
    getRole: () => currentRole,
    getToken: () => token,
    isAdmin: () => currentRole === "Admin"
  };
})();

if (typeof module !== "undefined") {
  module.exports = authService;
}