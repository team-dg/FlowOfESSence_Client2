using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

//http://answers.unity3d.com/questions/14112/poly-and-vertex-counting-at-runtime.html

namespace mset {
	public enum Corner {
		TopLeft,
		TopRight,
		TopCenter,
		BottomLeft,
		BottomRight,
		BottomCenter,
		Center
	}

	public class InfoMenu : MonoBehaviour {
		[Header ("Images")]
		[Tooltip ("Main Image")]
		public Texture2D infoTexture;
		private Texture2D infoTextureStore;

		[Tooltip ("Replacement image for tooggle")]
		private Texture2D infoDot;
		private Rect texRect = new Rect (0, 0, 0, 0);
		private Rect fpsRect = new Rect (0, 0, 0, 0);
		[Tooltip ("Placement for Image on screen")]
		public Corner placement = Corner.BottomLeft;
		public Color color = Color.white;
		public Vector2 logoPixelOffset = new Vector2 (0, 0);
		public Vector2 logoPercentOffset = new Vector2 (0, 0);
		//[Space(10, order=4)]

		[Header ("Enable AssetStore Linking")]
		[Tooltip ("If toggle by pressing H is enabled")]
		public bool toggleInfo = false;
		[Tooltip ("If linking is enabled")]
		public bool linkInfo = false;
		[Tooltip ("If shows at the start of the scene")]
		public bool activeAtStart = false;
		[Tooltip ("Link for UAS")]
		public string uASLink = ""; //content/17849

		[Header ("Active Statistic Counter")]
		[Tooltip ("Enable FPS Counter instead of image")]
		public bool stiatistiCounter = false;

		public float statisticsWidth = 80;
		public float statisticsHeight = 60;

		private float FPS;
		private float FPSms;
		[Tooltip ("Placement for FPS Counter")]
		public Corner placementFPS = Corner.BottomLeft;
		private GUIStyle fpsFont;
		private GUIStyle centerLink;
		[Tooltip ("FPS Text Font")]
		public Font fpsFontResource;
		[Tooltip ("FPS Text Size")]
		[Range (8, 14)]
		public int fontSize = 10;
		[Tooltip ("FPS Text Color")]
		public Color fpsColor = new Color (0.737f, 0.737f, 0.737f, 1);

		private GameObject[] allObjects;
		private float totalPoly;
		private int totalRealPoly;
		private int totalVertex;

		private bool toggleInfoHbool = false;
		private string display;

		private Camera mainCamera;

		void Reset () {
			infoTexture = Resources.Load ("renderedLogo") as Texture2D;
		}

		void Start () {
			mainCamera = Camera.main;
			fpsFont = new GUIStyle ();
			fpsFont.fontSize = fontSize;
			fpsFont.font = fpsFontResource;
			fpsFont.normal.textColor = fpsColor;
			fpsFont.alignment = TextAnchor.UpperCenter;

			infoTextureStore = infoTexture;

			if (mainCamera == false) {
				infoTexture = infoDot;
			}

			infoDot = new Texture2D (1, 1, TextureFormat.ARGB32, false);
			infoDot.SetPixel (1, 1, Color.clear);
			infoDot.Apply ();

		}
		void Update () {

			float polyCount = 0;
			int realpolyCount = 0;
			int vertCount = 0;

			allObjects = (GameObject[]) GameObject.FindObjectsOfType (typeof (GameObject));

			foreach (GameObject obj in allObjects) {
				Renderer rend = obj.GetComponent<Renderer> ();
				if (rend && rend.isVisible) {
					MeshFilter mf = obj.GetComponent<MeshFilter> ();
					if (mf) {
						polyCount += mf.mesh.triangles.Length / 3;
						vertCount += mf.mesh.vertexCount;
						realpolyCount += mf.mesh.vertexCount / 3;
					}
				}
			}
			totalPoly = polyCount;
			totalRealPoly = realpolyCount;
			totalVertex = vertCount;

			FPS = 1f / Time.unscaledDeltaTime; // fixed <60
			FPSms = Time.unscaledDeltaTime * 1000.0f;

			if (toggleInfo == true) {

				if (Input.GetKeyUp ("h") && toggleInfoHbool == false) {
					toggleInfoHbool = true;
				} else if (Input.GetKeyUp ("h") && toggleInfoHbool == true) {
					toggleInfoHbool = false;
				}

			}

		}

		void updateTexRect () {

			if (infoTexture) {

				float tw = infoTexture.width;
				float th = infoTexture.height;
				float cw = 0f;
				float ch = 0f;

				if (mainCamera.enabled == false) {
					infoTexture = infoDot;

				}
				if (mainCamera.enabled == true) {
					infoTexture = infoTextureStore;

					if (toggleInfoHbool == false) {
						infoTexture = infoTextureStore;
					} else {
						infoTexture = infoDot;
					}
				}

				if (mainCamera == true) {
					//check attached camera first
					cw = mainCamera.pixelWidth;
					ch = mainCamera.pixelHeight;
				}

				float ox = logoPixelOffset.x + logoPercentOffset.x * cw * 0.01f;
				float oy = logoPixelOffset.y + logoPercentOffset.y * ch * 0.01f;

				switch (placement) {
					case Corner.TopLeft:
						texRect.x = ox;
						texRect.y = oy;
						break;
					case Corner.TopRight:
						texRect.x = cw - ox - tw;
						texRect.y = oy;
						break;
					case Corner.TopCenter:
						texRect.x = (cw - ox - tw) / 2;
						texRect.y = oy;
						break;
					case Corner.BottomLeft:
						texRect.x = ox;
						texRect.y = ch - oy - th;
						break;
					case Corner.BottomRight:
						texRect.x = cw - ox - tw;
						texRect.y = ch - oy - th;
						break;
					case Corner.BottomCenter:
						texRect.x = (cw - ox - tw) / 2;
						texRect.y = ch - oy - th;
						break;
					case Corner.Center:
						texRect.x = (cw - ox - tw) / 2;
						texRect.y = (ch - oy - th) / 2;
						break;
				};
				texRect.width = tw;
				texRect.height = th;
			}

			float fpstw = statisticsWidth;
			float fpsth = statisticsHeight;

			float fpscw = 0f;
			float fpsch = 0f;
			fpscw = mainCamera.pixelWidth;
			fpsch = mainCamera.pixelHeight;
			float fpsox = fpscw * 0.01f;
			float fpsoy = fpsch * 0.01f;

			switch (placementFPS) {
				case Corner.TopLeft:
					fpsRect.x = fpsox;
					fpsRect.y = fpsoy;
					break;
				case Corner.TopRight:
					fpsRect.x = fpscw - fpsox - fpstw;
					fpsRect.y = fpsoy;
					break;
				case Corner.TopCenter:
					fpsRect.x = (fpscw - fpsox - fpstw) / 2;
					fpsRect.y = fpsoy;
					break;
				case Corner.BottomLeft:
					fpsRect.x = fpsox;
					fpsRect.y = fpsch - fpsoy - fpsth;
					break;
				case Corner.BottomRight:
					fpsRect.x = fpscw - fpsox - fpstw;
					fpsRect.y = fpsch - fpsoy - fpsth;
					break;
				case Corner.BottomCenter:
					fpsRect.x = (fpscw - fpsox - fpstw) / 2;
					fpsRect.y = fpsch - fpsoy - fpsth;
					break;
			};
			fpsRect.width = fpstw;
			fpsRect.height = fpsth;
		}

		void OnGUI () {
			updateTexRect ();

			if (infoTexture) {

				GUI.color = color;
				if (linkInfo == false) {
					GUI.DrawTexture (texRect, infoTexture);
				}

				if (linkInfo == true) {

					if (activeAtStart == true) {

						GUI.DrawTexture (texRect, infoTexture);

						Color tmpColor = GUI.color;
						GUI.color = new Color (1, 1, 1, 0.0f);

						if (GUI.Button (texRect, infoTexture)) {
							UnityEditorInternal.AssetStore.Open (uASLink);
						}
						GUI.color = tmpColor;
					}

				}

			}
			//http://answers.unity3d.com/questions/138464/how-to-make-a-line-break-in-a-gui-label.html
			display = "FPS : " + FPS.ToString ("#") + " ( " + FPSms.ToString ("#.00") + "ms )" + " \n Faces Tris: " + totalPoly.ToString () + " \n Vertex : " + totalVertex.ToString () + " \n Aprox Polys : " + totalRealPoly.ToString ();
			if (stiatistiCounter == true) {
				//GUI.Label(new Rect(10, 10, 100, 20), fpstext);
				GUI.Label (fpsRect, display, fpsFont);
			}
		}
	}
}