using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pux;
using Pux.Effects;
using Pux.UI;
using Pux.Resources;
using System;

/**
 * Singleton Class to handle all GUI Actions
 */
public class GUIManager : MonoBehaviour {
	private static GUIManager instance;
	
	/*private CornerButtonClickBehaviour buttonC;
	private CornerButtonBehaviourE buttonE;
	private CornerButtonBehaviourQ buttonQ;
	private CornerButtonBehaviourY buttonY;*/
	
	public Button buttonC;
	public Button buttonE;
	public Button buttonQ;
	public Button buttonY;
	
	public TextPanel PointsDisplay;

	private string symbolChain;
	private Time textTimer;
	private int poorMansBarrier;
	
	/*private Vector2 buttonCpos;
	private Vector2 buttonEpos;
	private Vector2 buttonQpos;
	private Vector2 buttonYpos;*/
	
	public float ButtonSlideDistance {
		get;
		set;
	}

	public AlertTextPanel AlertTextEntity;

	
	public enum Directions{
		Left,
		Right
	}
	
	private List<Vector2> positions;

	public event EventHandler<SymbolEventArgs> SymbolsChanged;
	public event EventHandler<SwipeEventArgs> SwipeCommitted;
	
	public static GUIManager Instance{
		get; 
		private set;
	}
	
	
	private void Awake(){
		Instance = this;
		checkAssumptions();
		InitComponents();
		InitButtons();
		ButtonSlideDistance = 181; // magnitude of Vector2(128,128) 
	}
	
	private void checkAssumptions(){
		if(PointsDisplay == null){
			Debug.LogError("GUI_Manger has no PointsDisplay Assigned");
		}
		if(buttonC == null){
			Debug.LogError("GUIManager has no buttonC assigned");
		}
		if(buttonY == null){
			Debug.LogError("GUIManager has no buttonY assigned");
		}
		if(buttonQ == null){
			Debug.LogError("GUIManager has no buttonQ assigned");
		}
		if(buttonE == null){
			Debug.LogError("GUIManager has no buttonE assigned");
		}
		if(AlertTextEntity == null){
			Debug.LogError("GUIManager has no AlertTextPanel assigned");
		}
		
	}
	
	
	
	private void InitComponents() {
		positions = new List<Vector2>();
	}

//	private void Reset() {
//		buttonC.positionX = (int)buttonCpos.x;
//		buttonC.positionY = (int)buttonCpos.y;
//		
//		buttonE.positionX = (int)buttonEpos.x;
//		buttonE.positionY = (int)buttonEpos.y;
//		
//		buttonQ.positionX = (int)buttonQpos.x;
//		buttonQ.positionY = (int)buttonQpos.y;
//		
//		buttonY.positionX = (int)buttonYpos.x;
//		buttonY.positionY = (int)buttonYpos.y;
//		
//	}
	private void InitButtons() {
		/*buttonC = gameObject.GetComponentInChildren<CornerButtonBehaviourC>();
		buttonY = gameObject.GetComponentInChildren<CornerButtonBehaviourY>();
		buttonQ = gameObject.GetComponentInChildren<CornerButtonBehaviourQ>();
		buttonE = gameObject.GetComponentInChildren<CornerButtonBehaviourE>();*/
		
		StorePositions();
	}
	
	private void StorePositions()
	{
		positions.Clear();
		positions.Add(buttonC.Position);
		positions.Add(buttonE.Position);
		positions.Add(buttonQ.Position);
		positions.Add(buttonY.Position);
	}
	
	
	private void OnButtonsSlidOut(Action action, ClockRotations rotation)
	{
		
		Debug.LogWarning("On ButtonsSlideOut need to be implemented again");
		/*poorMansBarrier ++;
		if (poorMansBarrier < 4) {
			return;
		}
		
		StorePositions();
		SwapTextures(rotation);
		if (action != null) {
			action();
		}*/
	}
	
	private void SwapTextures(ClockRotations rotation)
	{
		/*SwapTexture(buttonC, "green", rotation);
		SwapTexture(buttonE, "red", rotation);
		SwapTexture(buttonQ, "yellow", rotation);
		SwapTexture(buttonY, "purple", rotation);*/
	}
	
	private void SwapTexture(Button button, string color, ClockRotations rotation)
	{
		Debug.LogWarning("Swap Textures need to be implemented again");
		/*var path = "iPhone/UI/";
		var ns = string.Empty;
		if (button.Position.x < 480) {
			ns = rotation == ClockRotations.Clockwise ? "bottom" : "top";
		} else {
			ns = rotation == ClockRotations.Clockwise ? "top" : "bottom";
		}
		
		var sw = string.Empty;
		if (button.Position.y > 320) {
			sw = rotation == ClockRotations.Clockwise ? "right" : "left";
		} else {
			sw = rotation == ClockRotations.Clockwise ? "left" : "right";
		}
		
		var normal = string.Format("{0}arrow_{1}_{2}_{3}", path, color, ns, sw);
		var hover = normal + "_hover";
		
		button.activeStyle.normal.background = ResourceManager.GetResource<Texture2D>(normal);
		button.hoverStyle.normal.background = ResourceManager.GetResource<Texture2D>(hover);
		button.inactiveStyle.normal.background = ResourceManager.GetResource<Texture2D>(normal);*/
	}

	public void PerformUIRotation(ClockRotations clockRotation)
	{
		poorMansBarrier = 0;
		if (clockRotation == ClockRotations.Clockwise) {
			SlideButtonsOut(() => OnButtonsSlidOut(RotateRight, clockRotation));
		} else{
			SlideButtonsOut(() => OnButtonsSlidOut(RotateLeft, clockRotation));
		}
	}
	
	private void OnButtonsRotated()
	{
		SlideButtonsIn();
	}

	private void RotateLeft() {
		Vector2 tmp = positions[positions.Count - 1];
		positions.Insert(0, tmp);
		positions.RemoveAt(positions.Count - 1);
		UpdatePositions();
		OnButtonsRotated();
	}
	
	private void RotateRight() {
		positions.Add(positions[0]);
		positions.RemoveAt(0);
		UpdatePositions();
		OnButtonsRotated();
	}
	
	private void UpdatePositions() {
		
		buttonC.Position = positions[0];
		buttonE.Position = positions[1];
		buttonQ.Position = positions[2];
		buttonY.Position = positions[3];		
		
		
	}
	
	private void SlideButtonsOut(Action postAction)
	{
		PerformButtonSlide(buttonC, SlideDirections.Out, postAction);
		PerformButtonSlide(buttonE, SlideDirections.Out, postAction);
		PerformButtonSlide(buttonQ, SlideDirections.Out, postAction);
		PerformButtonSlide(buttonY, SlideDirections.Out, postAction);
	}
	
	private void SlideButtonsIn()
	{
		PerformButtonSlide(buttonC, SlideDirections.In, null);
		PerformButtonSlide(buttonE, SlideDirections.In, null);
		PerformButtonSlide(buttonQ, SlideDirections.In, null);
		PerformButtonSlide(buttonY, SlideDirections.In, null);
	}
	
	private Vector2 GetSnapPositionForButton(Button button)
	{
		Debug.LogWarning("GetSnapPositionsForButton need to be implemented again");
		return new Vector2(0,0);
		/*	// left
		if (button.Position.x < 480) {
			if (button.Position.y > 320) {
				return new Vector2(0, 640 - button.Height);
			} else {
				return new Vector2(0, 0);
			}
		} else {
			if (button.Position.y > 320) {
				return new Vector2(960 - button.Width, 640 - button.Height);
			} else {
				return new Vector2(960 - button.Width, 0);
			}
		}*/
	}
	
	private void PerformButtonSlide(Button button, SlideDirections direction, Action postAction)
	{
		Debug.LogWarning("PerformSlide need to be implemented again");
		/*var retinaCenter = new Vector2(480, 320);
		var buttonCenter = new Vector2(button.Position.x + (float) button.Width / 2.0f, button.Position.y + (float) button.Height / 2.0f );

		var directingVector = buttonCenter - retinaCenter;
		directingVector.Normalize();
		
		var sign = direction == SlideDirections.Out ? 1 : -1;
		var targetPosition =  buttonCenter + sign * directingVector * ButtonSlideDistance;
		
		//decentralize position
		targetPosition -= new Vector2((float)button.Width / 2.0f, (float)button.Height / 2.0f);
		if (direction == SlideDirections.In) {
			// need to deal with rounding errors
			targetPosition = GetSnapPositionForButton(button);
		}
		
		// set different easing functions for sliding in and out
		Func<float,float> easingFunc = null;
		if (direction == SlideDirections.Out) {
			easingFunc = (x) => x * x;
		} else {
			easingFunc = (x) => (float)(Math.Log(x) + 6.0f) / 6.0f;
		}
		
		var controller = new UIElementSlideController(easingFunc) {
			StartPosition = button.Position,
			TargetPosition = targetPosition,
			Duration = TimeSpan.FromMilliseconds(250)
		};
		
		if (postAction != null) {
			controller.ControllerFinished += (sender, e) => postAction.Invoke();
		}
		
		button.QueueController(button.name, controller);*/
	}

	public void ClearSymbols() {
		symbolChain = string.Empty;
		InvokeSymbolsChanged();
	}

	public void Alert(string value) {
		Debug.Log("Alert");
		AlertTextEntity.ShowText(value);
	}

	public void NotifyButtonHit(string symbol) {
		symbolChain += symbol;
		InvokeSymbolsChanged();
	}

	public void DisplayPoints(float points) {
		/*if (PointsDisplay == null)
			PointsDisplay = gameObject.GetComponentInChildren<PointsDisplay>();*/
		PointsDisplay.Text = points.ToString();
	}

	/* Not Needed any more -> Balloons
	public void DisplayLife(float life) {
		if (pointsDisplay == null)
			pointsDisplay = gameObject.GetComponentInChildren<PointsDisplay>();
		pointsDisplay.Life = life;
	}*/

// Invoke Events
	public void PreSwipeCommitted(Vector2 direction) {
		InvokeSwipeCommitted(direction);
	}

	private void InvokeSymbolsChanged() {
		var handler = SymbolsChanged;
		if (handler == null) {
			return;
		}
		
		var e = new SymbolEventArgs(symbolChain);
		SymbolsChanged(this, e);
	}

	private void InvokeSwipeCommitted(Vector2 direction) {
		var handler = SwipeCommitted;
		if (handler == null) {
			return;
		}
		
		var e = new SwipeEventArgs(direction, symbolChain);
		SwipeCommitted(this, e);
	}
}
