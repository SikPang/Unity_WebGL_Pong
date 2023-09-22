mergeInto(LibraryManager.library, {

  UnityException: function (reason) {
	window.dispatchReactUnityEvent(
		"UnityException", reason
	);
  },

  ValidCheck: function (data) {
	window.dispatchReactUnityEvent(
		"ValidCheck", data
	);
  },

  ScorePoint: function (hitSide) {
	window.dispatchReactUnityEvent(
		"ScorePoint", hitSide
	);
  },

  MovePaddle: function (pos) {
	window.dispatchReactUnityEvent(
		"MovePaddle", pos
	);
  },

});