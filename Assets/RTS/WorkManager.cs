using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS {

	public static class WorkManager {
	
		public static Rect CalculateSelectionBox (Bounds selectionBounds, Rect playingArea) {
			float coordx = selectionBounds.center.x;
			float coordy = selectionBounds.center.y;
			float coordz = selectionBounds.center.z;

			float extentx = selectionBounds.extents.x;
			float extenty = selectionBounds.extents.y;
			float extentz = selectionBounds.extents.z;

			//determine the coord of the screen for the selection bounds
			List< Vector3 > corners = new List< Vector3 > ();
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3( coordx + extentx, coordy + extenty, coordz + extentz)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3( coordx + extentx, coordy + extenty, coordz - extentz)));

			corners.Add(Camera.main.WorldToScreenPoint(new Vector3( coordx + extentx, coordy - extenty, coordz + extentz)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3( coordx - extentx, coordy + extenty, coordz + extentz)));

			corners.Add(Camera.main.WorldToScreenPoint(new Vector3( coordx + extentx, coordy - extenty, coordz - extentz)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3( coordx - extentx, coordy - extenty, coordz + extentz)));

			corners.Add(Camera.main.WorldToScreenPoint(new Vector3( coordx - extentx, coordy + extenty, coordz - extentz)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3( coordx - extentx, coordy - extenty, coordz - extentz)));

			//what are the bounds on the screen for the selection bounds
			Bounds screenBounds = new Bounds (corners[0], Vector3.zero);
			for (int i = 1; i < corners.Count; i++) {
				screenBounds.Encapsulate (corners [i]);
			}

			// REMEMBER screen coord start at bottom left corner
			float selectBoxTop = playingArea.height - (screenBounds.center.y + screenBounds.extents.y);
			float selectBoxLeft = screenBounds.center.x - screenBounds.extents.x;
			float selectBoxWidth = 2 * screenBounds.extents.x;
			float selectBoxHeight = 2 * screenBounds.extents.y;

			return new Rect (selectBoxLeft, selectBoxTop, selectBoxWidth, selectBoxHeight);
		}

	}

}
