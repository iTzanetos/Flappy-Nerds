using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;

public class KinectData
{
    // Make a global variable for the kinect sensor so that all parts of the program can use it
    public static KinectSensor kinect = null;

    public static Skeleton[] skeletonData;

    private static float leftHandY;
    private static float rightHandY;
    private static float headY;
    private static float hipCentreY;

    public KinectData()
    {
        kinect = null;
        leftHandY = 0;
        rightHandY = 0;
        headY = 0;
        hipCentreY = 0;
    }

    public void StartKinect()
    {
        kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected); // Get first Kinect Sensor
        kinect.SkeletonStream.Enable(); // Enable skeletal tracking

        skeletonData = new Skeleton[6]; // Allocate ST data

        kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(Kinect_SkeletonFrameReady); // Get Ready for Skeleton Ready Events

        kinect.Start(); // Start Kinect sensor
    }

    public static void StopKinect() // Stops the kinect sensor
    {
        if (kinect != null)
        {
            kinect.Stop();
            kinect.AudioSource.Stop();
        }
    }

    public void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
    {
        using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame()) // Open the Skeleton frame
        {
            if (skeletonFrame != null && skeletonData != null) // check that a frame is available
            {
                skeletonFrame.CopySkeletonDataTo(skeletonData); // get the skeletal information in this frame
            
                leftHandY = skeletonData[0].Joints[JointType.HandLeft].Position.Y;
                rightHandY = skeletonData[0].Joints[JointType.HandRight].Position.Y;
                headY = skeletonData[0].Joints[JointType.Head].Position.Y;
                hipCentreY = skeletonData[0].Joints[JointType.HipCenter].Position.Y;
            }
        }
    }

    public float GetLeftHandY()
    {
        return leftHandY;
    }

    public float GetRightHandY()
    {
        return rightHandY;
    }

    public float GetHeadY()
    {
        return headY;
    }

    public float GetHipY()
    {
        return hipCentreY;
    }

    
}