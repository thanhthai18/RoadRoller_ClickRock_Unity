using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadRoller_RoadRollerMinigame1 : MonoBehaviour
{
    public SkeletonAnimation anim;
    [SpineAnimation] public string anim_Run, anim_Break;

    public Vector2 startPos;
    


    private void Start()
    {
        startPos = transform.position;
        anim.state.Complete += AnimComplete;
        PlayAnim(anim, anim_Run, true);
    }

    private void AnimComplete(Spine.TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == anim_Run)
        {
            PlayAnim(anim, anim_Run, true);
        }
        if (trackEntry.Animation.Name == anim_Run && GameController_RoadRollerMinigame1.instance.isLose)
        {
            anim.state.TimeScale = 0;
        }
    }

    public void PlayAnim(SkeletonAnimation anim, string nameAnim, bool loop)
    {
        anim.state.SetAnimation(0, nameAnim, loop);
    }

    public void PlayAnimRun()
    {
        PlayAnim(anim, anim_Run, false);
    }
}
