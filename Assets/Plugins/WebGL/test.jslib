mergeInto(LibraryManager.library, {

  GameOver: function () {
	window.dispatchReactUnityEvent(
		"GameOver"
	);
  },

});