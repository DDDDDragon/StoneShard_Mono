using StoneShard_Mono_RoomEditor.Content;

namespace StoneShard_Mono_RoomEditor.Content.NPCs
{
    public class NPC : Entity
    {
        public bool ActionDone;

        public virtual void DoAction()
        {
            DoneAction();
        }

        public virtual void DoneAction()
        {
            ActionDone = true;
        }
    }
}
