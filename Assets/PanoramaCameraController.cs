using UnityEngine;
using System.Collections;

public class PanoramaCameraController : MonoBehaviour
{
    private Camera mCamera;

    //方向灵敏度  
    private float mTouchSensitivity = 0.3f;
    private float mMouseSensitivity = 5F;

    //上下最大视角(Y视角)  
    private float minimumY = -60F;
    private float maximumY = 60F;

    private float rotationY = 0F;
    
    // Use this for initialization
    void Start ()
    {
        mCamera = this.GetComponent<Camera>();

        if (mCamera == null)
        {
            mCamera = Camera.main;
        }

        rotationY = -mCamera.transform.localEulerAngles.x;
    }
	
	// Update is called once per frame
	void Update () {
        if(Application.platform == RuntimePlatform.Android)
        {
            TouchControl();
        }
        else
        {
            MouseControl();
        }
    }

    void MouseControl()
    {
        //鼠标右键
        if (Input.GetMouseButton(0))
        {
            //根据鼠标移动的快慢(增量), 获得相机左右旋转的角度(处理X)  
            float rotationX = mCamera.transform.localEulerAngles.y - Input.GetAxis("Mouse X") * mMouseSensitivity;

            //根据鼠标移动的快慢(增量), 获得相机上下旋转的角度(处理Y)  
            rotationY -= Input.GetAxis("Mouse Y") * mMouseSensitivity;
            //角度限制. rotationY小于min,返回min. 大于max,返回max. 否则返回value   
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            //总体设置一下相机角度  
            mCamera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0); 
        }
    }

    void TouchControl()
    {
        if (Input.touchCount == 1)//一个手指触摸屏幕
        {
            if (Input.touches[0].phase == TouchPhase.Moved)//手指移动
            {
                float rotationX = mCamera.transform.localEulerAngles.y - Input.touches[0].deltaPosition.x * mTouchSensitivity;
                
                rotationY -= Input.touches[0].deltaPosition.y * mTouchSensitivity; 
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                mCamera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0); 
            }
        }
    }
}
