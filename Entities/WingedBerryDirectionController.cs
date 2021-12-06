using Celeste;
using System;
using Celeste.Mod.Entities;
using System.Collections;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;


namespace Celeste.Mod.SorbetHelper {

    	[CustomEntity("SorbetHelper/WingedBerryDirectionController")]
        [Tracked]
        public class WingedBerryDirectionController : Entity {

            private bool updateHooked = false;
            private int direction = 0;
            // 1 = Right
            // 2 = Left
            // 3 = Down
            // Anything Else = Up

            public WingedBerryDirectionController(EntityData data, Vector2 offset) {
                direction = data.Int("direction");
                Visible = false;
            }

            public override void Awake(Scene scene) {
                base.Awake(scene);

                Logger.Log("SorbetHelper", "Hooking FlyAwayRoutine");
                //On.Celeste.Strawberry.FlyAwayRoutine += Strawberry_FlyAwayRoutine;
                //On.Celeste.Strawberry.OnDash += OnDashHooked;
                if (!updateHooked) {
                    On.Celeste.Strawberry.Update += StrawberryUpdate;
                    updateHooked = true;
                }
                Logger.Log("SorbetHelper", "Hooked FlyAwayRoutine");
                /*if (scene.Tracker.GetEntity<Strawberry>() != null) {
                    Logger.Log("SorbetHelper", "Found Strawberry!");
                    On.Celeste
                }*/
            }

            private void StrawberryUpdate(On.Celeste.Strawberry.orig_Update orig, Strawberry self) {
                DynData<Strawberry> strawberryData = new DynData<Strawberry>(self);
                float origFlapSpeed = strawberryData.Get<float>("flapSpeed");
                //strawberryData["flapSpeed"] = 0f;//origFlapSpeed - origFlapSpeed * 2;
                orig(self);
                strawberryData["flapSpeed"] = origFlapSpeed;

                //self.X = self.X + 5f;}

             //   Logger.Log("SorbetHelper","The Nerry starts update!!!!!");
               // if(self.WaitingOnSeeds) {
                 //   Logger.Log("SorbetHelper","The Nerry has run the return oh no this is bad bad abd!!!! Neyyr!");
                 //   return;
                //}
                if(!strawberryData.Get<bool>("collected") && self.Follower.Leader != null && self.Follower.DelayTimer <= 0f && StrawberryRegistry.IsFirstStrawberry(self)) {
                } else if (self.Winged) {
                  //  Logger.Log("SorbetHelper","The Nerry is moving1");
                    self.X += origFlapSpeed * Engine.DeltaTime;
                    if(!strawberryData.Get<bool>("flyingAway")) {
                        origFlapSpeed = Calc.Approach(origFlapSpeed, 20f, 170f * Engine.DeltaTime);
                        Vector2 start = strawberryData.Get<Vector2>("start");
                       // Logger.Log("SorbetHelper","The Nerry is not flying away!");
						if (self.X < start.X - 5f)
							{
							    self.X = start.X - 5f;
							}
							else if (self.X > start.X + 5f)
							{
								self.X = start.X + 5f;
							}
                    }

                }
                               // Logger.Log("SorbetHelper","The Nerry is about to be origional !!!!!!");

                //orig(self);
                //strawberryData["flapSpeed"] = origFlapSpeed;
            }

            /*private void OnDashHooked(On.Celeste.Strawberry.orig_OnDash orig, Strawberry self, Vector2 dir) {
                DynData<Strawberry> strawberryData = new DynData<Strawberry>(self);
                bool flyingAway = strawberryData.Get<bool>("flyingAway");
                if (!flyingAway && self.Winged && !self.WaitingOnSeeds) {
				    Depth = -1000000;
				    Add(new Coroutine(FlyAwayRoutine(self)));
				    flyingAway = true;
			    }
                strawberryData["flyingAway"] = flyingAway;
            }

            private IEnumerator FlyAwayRoutine(Strawberry self) {
                DynData<Strawberry> strawberryData = new DynData<Strawberry>(self);
                Wiggler rotateWiggler = strawberryData.Get<Wiggler>("rotateWiggler");
                float flapSpeed = strawberryData.Get<float>("flapSpeed");
                Follower Follower = strawberryData.Get<Follower>("Follower");
			    rotateWiggler.Start();
			    flapSpeed = -200f;
			    Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 0.25f, start: true);
			    tween.OnUpdate = delegate(Tween t) {
			    	flapSpeed = MathHelper.Lerp(-200f, 0f, t.Eased);
			    };
                                Logger.Log("SorbetHelper", "Just Started The Berry Fly!");

			    Add(tween);
			    yield return 0.1f;
                                Logger.Log("SorbetHelper", "Unhdasffeffooking FlyAwayRoutine");

			    Audio.Play("event:/game/general/strawberry_laugh", Position);
			    yield return 0.2f;
			    if (!Follower.HasLeader) {
			    	Audio.Play("event:/game/general/strawberry_flyaway", Position);
			    }
			    tween = Tween.Create(Tween.TweenMode.Oneshot, null, 0.5f, start: true);
			    tween.OnUpdate = delegate(Tween t) {
			    	flapSpeed = MathHelper.Lerp(0f, -200f, t.Eased);
			    };
                                Logger.Log("SorbetHelper", "Unhaaaaaaaaaaaaaaaaaaaabcooking FlyAwayRoutine");

			    Add(tween);
                                Logger.Log("SorbetHelper", "Uadsdas:)))))))))ing FlyAwayRoutine");

                strawberryData["rotateWiggler"] = rotateWiggler;
                strawberryData["flapSpeed"] = flapSpeed;
                strawberryData["Follower"] = Follower;
		    }

            private static IEnumerator Strawberry_FlyAwayRoutine(On.Celeste.Strawberry.orig_FlyAwayRoutine orig, Strawberry self) {
                //orig(self);
                DynData<Strawberry> strawberryData = new DynData<Strawberry>(self);
                float flapSpeed = strawberryData.Get<float>("flapSpeed");
                Console.WriteLine(flapSpeed);
                flapSpeed = -200f;
                strawberryData["flapSpeed"] = flapSpeed;
                Console.WriteLine(flapSpeed);
                Logger.Log("SorbetHelper", "Just Started The Berry Fly!");
                yield return new SwapImmediately(orig(self));
                flapSpeed = 200f;
                Console.WriteLine(flapSpeed);
            }*/

            public override void Removed(Scene scene) {
                base.Removed(scene);
                Logger.Log("SorbetHelper", "Unhooking FlyAwayRoutine");
                if (updateHooked) {
                    On.Celeste.Strawberry.Update -= StrawberryUpdate;
                    updateHooked = false;
                }
                //On.Celeste.Strawberry.OnDash -= OnDashHooked;
               // On.Celeste.Strawberry.FlyAwayRoutine -= Strawberry_FlyAwayRoutine;
                Logger.Log("SorbetHelper", "Unhooked FlyAwayRoutine");

            }
            public override void SceneEnd(Scene scene) {
                base.SceneEnd(scene);
                Logger.Log("SorbetHelper", "SceneEnd: Unhooking FlyAwayRoutine");
                if (updateHooked) {
                    On.Celeste.Strawberry.Update -= StrawberryUpdate;
                    updateHooked = false;
                }
                //On.Celeste.Strawberry.OnDash -= OnDashHooked;
               // On.Celeste.Strawberry.FlyAwayRoutine -= Strawberry_FlyAwayRoutine;
                Logger.Log("SorbetHelper", "SceneEnd: Unhooked FlyAwayRoutine");

            }
        }

}