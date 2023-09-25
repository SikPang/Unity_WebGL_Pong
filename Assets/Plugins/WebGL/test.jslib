mergeInto(LibraryManager.library, {

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

  ScorePoint: function (data) {
	window.dispatchReactUnityEvent(
		"ScorePoint", data
	);
  },

  MovePaddle: function (data) {
	window.dispatchReactUnityEvent(
		"MovePaddle", data
	);
  },

});