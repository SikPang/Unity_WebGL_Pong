mergeInto(LibraryManager.library, {

  Init: function () {
	window.dispatchReactUnityEvent(
		"Init"
	);
  },

  UnityException: function (data) {
	window.dispatchReactUnityEvent(
		"UnityException", data
	);
  },

  ValidCheck: function (data) {
	window.dispatchReactUnityEvent(
		"ValidCheck", data
	);
  },

  MovePaddle: function (data) {
	window.dispatchReactUnityEvent(
		"MovePaddle", data
	);
  },

});