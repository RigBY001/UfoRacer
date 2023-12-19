using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HUDIndicator {
	public class BarCanvasOnScreen : IndicatorCanvas {

        private IndicatorBarOnScreen indicatorOnScreen;

        // Icon variables
        private Slider barSlider;
        private RectTransform rectTransform;
        private IndicatorBarStyle style;

        public Slider BarSlider=>barSlider;
        
		public override void Create(Indicator indicator, IndicatorRenderer renderer) {
			base.Create(indicator, renderer);

            indicatorOnScreen = indicator as IndicatorBarOnScreen;

            // Get indicator style
            style = indicatorOnScreen.style;

            // Create game object
            // gameObject = new GameObject($"IndicatorBarOnScreen:{indicator.gameObject.name}");
            barSlider = Object.Instantiate(style.barSlider);
            gameObject = barSlider.gameObject;
            gameObject.name = $"IndicatorBarOnScreen:{indicator.gameObject.name}";
            gameObject.transform.SetParent(renderer.transform);

            // Setup rect transform
            rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            // Create icon image

            // Update icon style
            UpdateStyle();
        }

		public override void Update() {
            if(!active) return;

            if(IsVisible()) {
                UpdateStyle();
                UpdatePosition();
            }
			else {
                if(gameObject.activeSelf) {
                    gameObject.SetActive(false);
				}
            }
        }

        private void UpdateStyle() {
            rectTransform.sizeDelta = new Vector2(style.width, style.height);
        }

        private void UpdatePosition() {
            Rect rendererRect = renderer.GetRect();
            Vector3 pos = renderer.GetRectTransform().InverseTransformPoint(renderer.camera.WorldToScreenPoint(indicator.gameObject.transform.position));
        
            rendererRect.x += style.width / 2f;
            rendererRect.y += style.height/ 2f;
            rendererRect.width -= style.width;
            rendererRect.height -= style.height;

            // On-screen (Show)
            if (renderer.GetRect().Contains(pos)) {
                gameObject.SetActive(true);
                
                rectTransform.position = renderer.GetRectTransform().TransformPoint(new Vector3(pos.x, pos.y, 0));
                     
            }
            // Off-screen (Hide)
            else {
                gameObject.SetActive(false);
            }   

            // // On-screen (Show)
            // if (pos.z >= 0 && pos.x >= rendererRect.x && pos.x <= rendererRect.x + rendererRect.width && pos.y >= rendererRect.y && pos.y <= rendererRect.y + rendererRect.height) {
            //     gameObject.SetActive(true);
                
            //     rectTransform.position = renderer.GetRectTransform().TransformPoint(new Vector3(pos.x, pos.y, 0));
                     
            // }
            // // Off-screen (Hide)
            // else {
            //     gameObject.SetActive(false);
            // }

        }

		public override void Destroy() {
			GameObject.Destroy(gameObject);
		}
	}
}
