using Celeste.Mod.Entities;
using Monocle;
using MonoMod.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;

namespace Celeste.Mod.SorbetHelper {

    //[CustomEntity("SorbetHelper/CustomSpring")]
    [CustomEntity(new string[] {
        "SorbetHelper/CustomSpringUp = LoadUp",
        "SorbetHelper/CustomSpringLeft = LoadLeft",
        "SorbetHelper/CustomSpringRight = LoadRight"
    })]
    public class CustomSpring : Spring {


		//private MTexture[] textures;
        public int size = 16;
        public static Entity LoadUp(Level level, LevelData levelData, Vector2 offset, EntityData entityData) =>
            new CustomSpring(entityData, offset, Orientations.Floor);
        public static Entity LoadLeft(Level level, LevelData levelData, Vector2 offset, EntityData entityData) =>
            new CustomSpring(entityData, offset, Orientations.WallLeft);
        public static Entity LoadRight(Level level, LevelData levelData, Vector2 offset, EntityData entityData) =>
            new CustomSpring(entityData, offset, Orientations.WallRight);
		private static MethodInfo springOnPuffer = typeof(Spring).GetMethod("OnPuffer", BindingFlags.Instance | BindingFlags.NonPublic);

        public CustomSpring(EntityData data, Vector2 offset, Orientations orientation) : base(data, offset, orientation) {
            DynData<CustomSpring> baseData = new DynData<CustomSpring>(this);
            /*Remove(baseData.Get<PlayerCollider>("OnCollide"));
            Remove(baseData.Get<PlayerCollider>("OnCollide"));
            Add(new PlayerCollider(OnCollide));
			Add(new HoldableCollider(OnHoldable));*/
            PufferCollider pufferCollider = new PufferCollider(OnPuffer);//baseData.Get<PufferCollider>("pufferCollider");
			//Remove(baseData.Get<PufferCollider>("pufferCollider"));
			Add(pufferCollider);
            //data.Int("size") * 8;
			size = data.Height;
            switch (orientation) {
			case Orientations.Floor:
				size = data.Width;
				base.Collider = new Hitbox(size, 6f, -8f, -6f);
				pufferCollider.Collider = new Hitbox(size, 10f, -8f, -10f);
				break;
			case Orientations.WallLeft:
				//size = data.Height;
				base.Collider = new Hitbox(6f, size, 0f, -8f);
				pufferCollider.Collider = new Hitbox(12f, size, 0f, -8f);
				//sprite.Rotation = (float)Math.PI / 2f;
				break;
			case Orientations.WallRight:
				//size = data.Height;
				base.Collider = new Hitbox(6f, size, -6f, -8f);
				pufferCollider.Collider = new Hitbox(12f, size, -12f, -8f);
				//sprite.Rotation = -(float)Math.PI / 2f;
				break;
			default:
				throw new Exception("Orientation not supported!");
			}
            //baseData.Set("pufferCollider", pufferCollider);
           // base.Collider = ;

        }
        /*public override void Render() {
			textures[0].Draw(Position - new Vector2(-8f, -8f));
			for (int i = 8; (float) i < size - 8f; i += 8) {
				textures[1].Draw(Position + new Vector2(i, 0f));
			}
			textures[3].Draw(Position + new Vector2(size - 0f, 8f));
			textures[2].Draw(Position + new Vector2(size / 2f - 4f, 0f));
		}

        public override void Added(Scene scene) {
            base.Added(scene);
			MTexture mTexture = GFX.Game["objects/SorbetHelper/customSpring/default"];
			textures = new MTexture[mTexture.Width / 8];
			for (int i = 0; i < textures.Length; i++) {
				textures[i] = mTexture.GetSubtexture(i * 8, 0, 8, 8);
			}
			scene.Add(new SinkingPlatformLine(Position + new Vector2(base.Width / 2f, base.Height / 2f)));
        }*/

		private void OnPuffer(Puffer p) {
			springOnPuffer.Invoke(this, new object[] { p });
		}

    }

}