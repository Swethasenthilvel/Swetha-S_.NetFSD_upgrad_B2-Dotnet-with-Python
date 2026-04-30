const employeeService = (() => {

  return {

    getAll: async function (params = {}) {
      return await storageService.getAll(params);
    },

    getById: async function (id) {
      return await storageService.getById(id);
    },

    add: async function (data) {
      return await storageService.add(data);
    },

    update: async function (id, data) {
      return await storageService.update(id, data);
    },

    remove: async function (id) {
      return await storageService.remove(id);
    }

  };

})();

if (typeof module !== "undefined") {
  module.exports = employeeService;
}