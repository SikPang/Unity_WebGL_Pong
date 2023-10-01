mergeInto(LibraryManager.library, {

  Init: function () {
	window.dispatchReactUnityEvent(
		"Init"
	);
  },

  UnityException: function (data) {
	window.dispatchReactUnityEvent(
		"UnityException", UTF8ToString(data)
	);
  },

  ValidCheck: function (data) {
	window.dispatchReactUnityEvent(
		"ValidCheck", UTF8ToString(data)
	);
  },

  MovePaddle: function (data) {
	window.dispatchReactUnityEvent(
		"MovePaddle", UTF8ToString(data)
	);
  },

  BallHit: function (data) {
	window.dispatchReactUnityEvent(
		"BallHit", UTF8ToString(data)
	);
  },

});