using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StoneShard_Mono.Managers;
using StoneShard_Mono.Content.Components;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Content.Rooms;
using StoneShard_Mono.Content.GameValues;

namespace StoneShard_Mono.Content.Players
{
    public abstract class Player : Entity
    {
        public void GoToRoom(Room room, Vector2 pos)
        {
            CurrentRoom = room;

            room.LocalPlayer = this;

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
        public GameValue<int> MainHandDamage;

        /// <summary>
        /// 副手伤害
        /// </summary>
        public GameValue<int> OffHandDamage;

        public GameValue<float> WeaponDamage;

        public GameValue<float> MainHandEfficiency;

        public GameValue<float> OffHandEfficiency;

        public GameValue<float> BodypartDamage;

        public GameValue<float> ArmorDamage;

        public GameValue<float> ArmorPenetration;

        public GameValue<float> Accuracy;

        public GameValue<float> CritChance;

        public GameValue<float> CritEfficiency;

        public GameValue<float> CounterChance;

        public GameValue<float> FumbleChance;

        public GameValue<float> SkillsEnergyCost;

        public GameValue<float> SpellsEnergyCost;

        public GameValue<float> CooldownsDuration;

        public GameValue<int> BonusRange;

        public GameValue<float> BleedChance;

        public GameValue<float> DazeChance;

        public GameValue<float> StunChance;

        public GameValue<float> KnockbackChance;

        public GameValue<float> ImmobilizationChance;

        public GameValue<float> StaggerChance;

        public GameValue<float> LifeDrain;

        public GameValue<float> EnergyDrain;

        public GameValue<float> ExperienceGain;

        public GameValue<int> Health;

        public GameValue<int> MaxHealth;

        public GameValue<float> HealthRestoration;

        public GameValue<float> HealingEfficiency;

        public GameValue<int> Energy;

        public GameValue<int> MaxEnergy;

        public GameValue<float> EnergyRestoration;

        public GameValue<int> Protection;

        public GameValue<float> BlockChance;

        public GameValue<int> BlockPower;

        public GameValue<int> MaxBlockPower;

        public GameValue<float> BlockPowerRecovery;

        public GameValue<float> DodgeChance;

        public GameValue<float> Stealth;

        public GameValue<float> NoiseProduced;

        public GameValue<float> Lockpicking_Disarming;

        public GameValue<int> Vision;

        public GameValue<float> Fortitude;

        public GameValue<float> DamageReflection;

        public GameValue<float> BleedResistance;

        public GameValue<float> ControlResistance;

        public GameValue<float> MoveResistance;

        public GameValue<float> HungerResistance;

        public GameValue<float> IntoxicationResistance;

        public GameValue<float> PainResistance;

        public GameValue<float> FatigueResistance;

        public GameValue<float> DamageTaken;

        public GameValue<float> PhysicalResistance;

        public GameValue<float> NatureResistance;

        public GameValue<float> MagicResistance;

        public GameValue<float> SlashingResistance;

        public GameValue<float> PiercingResistance;

        public GameValue<float> CrushingResistance;

        public GameValue<float> RendingResistance;

        public GameValue<float> FireResistance;

        public GameValue<float> PoisonResistance;

        public GameValue<float> FrostResistance;

        public GameValue<float> ShockResistance;

        public GameValue<float> CausticResistace;

        public GameValue<float> ArcaneResistace;

        public GameValue<float> SacredResistance;

        public GameValue<float> UnholyResistance;

        public GameValue<float> PsionicResistance;

        public GameValue<float> MagicPower;

        public GameValue<float> MiracleChance;

        public GameValue<float> MiraclePotency;

        public GameValue<float> BackfireChance;

        public GameValue<float> BackfireDamage;

        public GameValue<float> PyromanticPower;

        public GameValue<float> GeomanticPower;

        public GameValue<float> VenomanticPower;

        public GameValue<float> CryomanticPower;

        public GameValue<float> ElectromanticPower;

        public GameValue<float> ArcanisticPower;

        public GameValue<float> AstromanticPower;

        public GameValue<float> PsionicPower;

        public GameValue<float> ChronomanticPower;

        #endregion
    }
}
