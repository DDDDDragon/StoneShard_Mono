using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StoneShard_Mono.Managers;
using StoneShard_Mono.Content.Components;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Content.Rooms;

namespace StoneShard_Mono.Content.Players
{
    public abstract class Player : Entity
    {
        public void GoToRoom(Room room, Vector2 pos)
        {
            CurrentRoom = room;

            room.Player = this;

            room.RegisterEntity(this, pos);

            SetPos(pos);
        }

        public override void SetDefaults()
        {
            Timer = new Timer();

            CurrentPath = new();

            HeadNormal = Main.TextureManager[TexType.Entity, $"Player\\{ID}\\head_normal"];

            Shadow = Main.TextureManager[TexType.Entity, "shadow_small"];

            Direction = -1;

            _offsets = new Vector2[] { new(-1, -1), new(-1, 0), new(0, 1), new(1, 0), new(1, -1), new(1, 0), new(0, 1), new(-1, 0) };

            Rotation = 0;

            base.SetDefaults();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Shadow, Position + new Vector2((Width - Shadow.Width) / 2 + 6, Height + 2), null, Color.Black * 0.4f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1 - TilePosition.Y / 1000 + 0.0003f);

            var drawPos = Position + DrawOffset;

            spriteBatch.Draw(Body, drawPos + BodyOffset + Rectangle.Size.ToVector2() / 2 + new Vector2(6 * -Direction, 6), new Rectangle(0, Height * (int)Timer[2], Width, Height), Color.White,
                Rotation, Rectangle.Size.ToVector2() / 2, new Vector2(-Direction, 1f), SpriteEffects.None, 1 - TilePosition.Y / 1000 + 0.0002f);

            var headRect = new Rectangle(0, (HeadNormal.Height + (HurtHeavily ? 1 : 0)) / 4 * (int)Timer[1], HeadNormal.Width, HeadNormal.Height / 4);

            spriteBatch.Draw(HeadNormal, drawPos + HeadOffset + Rectangle.Size.ToVector2() / 2 + new Vector2(6 * -Direction, 6), headRect, Color.White,
                Rotation, Rectangle.Size.ToVector2() / 2, new Vector2(-Direction, 1f), SpriteEffects.None, 1 - TilePosition.Y / 1000 + 0.0001f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Timer[0]++; //Timer0 Timer1控制眨眼
            //Timer2 控制身体状态
            if (!IsMove) Timer[3]++; //Timer3 Timer4控制待机移动

            if (Timer[0] == 20 && Timer[1] == 1)
            {
                Timer[1] = 0;
                Timer[0] = 0;
            }
            else if (Timer[0] == 210)
            {
                Timer[1] = 1;
                Timer[0] = 0;
            }

            if (Timer[3] == 10)
            {
                if (Timer[4] < 7) Timer[4]++;
                if (Timer[4] == 7) Timer[4] = 0;
                DrawOffset = _offsets[(int)Timer[4]];
                Timer[3] = 0;
            }
        }

        public Texture2D HeadNormal;

        public Texture2D Body;

        public Texture2D Shadow;

        public Vector2 HeadOffset;

        public Vector2 BodyOffset;

        private Vector2[] _offsets;

        public bool HurtHeavily;

        public override Vector2 Position
        {
            get => (TilePosition + CurrentRoom.TilePostion) * Main.TileSize + new Vector2(0, Main.TileSize / 2 - Height);
        }

        public override int Width => Body.Width;

        public override int Height => Body.Height / 3;

        public override Rectangle Rectangle => new((int)Position.X, (int)Position.Y, Width + 12, Height + 12);

        public Timer Timer;

        #region 属性-Attributes

        /// <summary>
        /// 主手伤害
        /// </summary>
        public int MainHandDamage { get; set; }

        /// <summary>
        /// 副手伤害
        /// </summary>
        public int OffHandDamage { get; set; }

        public float WeaponDamage { get; set; }

        public float MainHandEfficiency { get; set; }

        public float OffHandEfficiency { get; set; }

        public float BodypartDamage { get; set; }

        public float ArmorDamage { get; set; }

        public float ArmorPenetration { get; set; }

        public float Accuracy { get; set; }

        public float CritChance { get; set; }

        public float CritEfficiency { get; set; }

        public float CounterChance { get; set; }

        public float FumbleChance { get; set; }

        public float SkillsEnergyCost { get; set; }

        public float SpellsEnergyCost { get; set; }

        public float CooldownsDuration { get; set; }

        public int BonusRange { get; set; }

        public float BleedChance { get; set; }

        public float DazeChance { get; set; }

        public float StunChance { get; set; }

        public float KnockbackChance { get; set; }

        public float ImmobilizationChance { get; set; }

        public float StaggerChance { get; set; }

        public float LifeDrain { get; set; }

        public float EnergyDrain { get; set; }

        public float ExperienceGain { get; set; }

        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public float HealthRestoration { get; set; }

        public float HealingEfficiency { get; set; }

        public int Energy { get; set; }

        public int MaxEnergy { get; set; }

        public float EnergyRestoration { get; set; }

        public int Protection { get; set; }

        public float BlockChance { get; set; }

        public int BlockPower { get; set; }

        public int MaxBlockPower { get; set; }

        public float BlockPowerRecovery { get; set; }

        public float DodgeChance { get; set; }

        public float Stealth { get; set; }

        public float NoiseProduced { get; set; }

        public float Lockpicking_Disarming { get; set; }

        public int Vision { get; set; }

        public float Fortitude { get; set; }

        public float DamageReflection { get; set; }

        public float BleedResistance { get; set; }

        public float ControlResistance { get; set; }

        public float MoveResistance { get; set; }

        public float HungerResistance { get; set; }

        public float IntoxicationResistance { get; set; }

        public float PainResistance { get; set; }

        public float FatigueResistance { get; set; }

        public float DamageTaken { get; set; }

        public float PhysicalResistance { get; set; }

        public float NatureResistance { get; set; }

        public float MagicResistance { get; set; }

        public float SlashingResistance { get; set; }

        public float PiercingResistance { get; set; }

        public float CrushingResistance { get; set; }

        public float RendingResistance { get; set; }

        public float FireResistance { get; set; }

        public float PoisonResistance { get; set; }

        public float FrostResistance { get; set; }

        public float ShockResistance { get; set; }

        public float CausticResistace { get; set; }

        public float ArcaneResistace { get; set; }

        public float SacredResistance { get; set; }

        public float UnholyResistance { get; set; }

        public float PsionicResistance { get; set; }

        public float MagicPower { get; set; }

        public float MiracleChance { get; set; }

        public float MiraclePotency { get; set; }

        public float BackfireChance { get; set; }

        public float BackfireDamage { get; set; }

        public float PyromanticPower { get; set; }

        public float GeomanticPower { get; set; }

        public float VenomanticPower { get; set; }

        public float CryomanticPower { get; set; }

        public float ElectromanticPower { get; set; }

        public float ArcanisticPower { get; set; }

        public float AstromanticPower { get; set; }

        public float PsionicPower { get; set; }

        public float ChronomanticPower { get; set; }

        #endregion
    }
}
