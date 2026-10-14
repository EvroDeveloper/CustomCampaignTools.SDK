using SLZ.Marrow.Interaction;
using SLZ.Marrow.Utilities;

namespace SimpleSerializables.Types
{
    public class MarrowEntityPoseSer
    {
        public SimpleTransformSer[] bodyPoses;

        public MarrowEntityPoseSer() {}
        public MarrowEntityPoseSer(MarrowEntityPose pose)
        {
            bodyPoses = new SimpleTransformSer[pose.bodyPoses.Length];
            for (int i = 0; i < pose.bodyPoses.Length; i++)
            {
                bodyPoses[i] = new(pose.bodyPoses[i]);
            }
        }

        public MarrowEntityPose ToMarrowEntityPose()
        {
            MarrowEntityPose pose = new();
            SimpleTransform[] normalBodyPose = new SimpleTransform[bodyPoses.Length];

            for (int i = 0; i < bodyPoses.Length; i++)
            {
                if(bodyPoses[i] == null)
                {
                    normalBodyPose[i] = new SimpleTransform();
                }
                normalBodyPose[i] = bodyPoses[i].ToSimpleTransform();
            }

            pose.bodyPoses = normalBodyPose;
            return pose;
        }
    }
}
