using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HUDIndicator {

	[AddComponentMenu("HUD Indicator/Indicator Bar On Screen")]
	public class IndicatorBarOnScreen : Indicator {
		public IndicatorBarStyle style;
        private Slider barSlider;
        
		protected override void CreateIndicatorCanvas(IndicatorRenderer renderer) {
			BarCanvasOnScreen indicatorCanvasOnScreen = new ();
			indicatorCanvasOnScreen.Create(this, renderer);
            barSlider = indicatorCanvasOnScreen.BarSlider;
			indicatorsCanvas.Add(renderer, indicatorCanvasOnScreen);
		}
        public void SetValue(float value){
            barSlider.value = Mathf.Clamp01(value);
        }
	}
}