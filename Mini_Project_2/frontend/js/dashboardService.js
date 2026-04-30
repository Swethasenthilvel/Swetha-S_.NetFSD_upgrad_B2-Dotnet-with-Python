const dashboardService = (() => {
  return {

    // GET DASHBOARD (all data from API)
    getSummary: async () => {
      return await storageService.getDashboard();
    }

  };
})();

if (typeof module !== 'undefined') module.exports = dashboardService;