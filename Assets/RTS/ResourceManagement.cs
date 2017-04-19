using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS {
	public static class ResourceManagement {
		// scrolling of the camera
		public static int sWidth { 
			get { return 15; } 
		}

		// speed of scrolling of camera
		public static float sSpeed {
			get { return 25; }
		}

		// rotation speed of the camera
		public static float rSpeed {
			get { return 100; }
		}

		// rotation amount of the camera
		public static float rAmount {
			get { return 10; }
		}

		//Limits to the scrolling of the camera
		public static float MinCamHeight {
			get { return 15; } 
		}
		public static float MaxCamHeight {
			get { return 50; } 
		}

		//InvalidPosition used in Mouse Actions
		private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
		public  static Vector3 InvalidPosition {
			get { return invalidPosition; }
		}

		// Reference to the DrawSelectionBox in the WorldObject Script
		private static GUISkin selectBoxSkin;
		public static GUISkin SelectBoxSkin {
			get { return selectBoxSkin; }
		}

		public static void StoreSelectBoxItems(GUISkin skin) {
			selectBoxSkin = skin;
		}

		//Invalid Bounds (used in selection box) 
		private static Bounds invalidBounds = new Bounds (new Vector3(-99999,-99999,-99999), new Vector3(0,0,0));
		public static Bounds InvalidBounds {
			get { return invalidBounds; }
		}
	}		
}
