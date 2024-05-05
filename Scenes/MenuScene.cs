using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneShard_Mono.Components;
using System.ComponentModel;

namespace StoneShard_Mono.Scenes
{
    public class MenuScene : Scene
    {
        public MenuScene() 
        {
            var loading = new StateContainer(new(Main.GameWidth, Main.GameHeight), (sender, args) =>
            {
                var loading = sender as StateContainer;
                if (loading.CurrentState == "MainMenu" || loading.CurrentState == "loading_3") return;

                var timer = loading.SelectChildById<Timer>("timer");
                var state = loading.SelectChildById<SizeContainer>("state");
                state.Alpha = 0;
                timer[0] = 10f;
                var page = int.Parse(loading.CurrentState.Replace("loading_", ""));
                loading.SwitchToState($"loading_{++page}");
            });

            loading.RegisterState("loading_0", new UIText("SSFont", new(Main.GameWidth, Main.GameHeight),
                Main.GetText("beta"), textHorizontalMiddle: true, textVerticalMiddle: true,
                splitCharacter: "\n", fontColor: Color.White, fontSize: 30));

            loading.RegisterState("loading_1", new UIImage("ISG_logo", horizontalMiddle: true, verticalMiddle: true, scale: 2));

            loading.RegisterState("loading_2", new UIImage("HT_logo", horizontalMiddle: true, verticalMiddle: true, scale: 2));

            var column = new ColumnContainer();
            column.RegisterChild(new UIText("SSFont", text: Main.GetText("save_top"), fontColor: Color.White, fontSize: 30, textVerticalMiddle: true));
            column.RegisterChild(new Space(1, 20));
            column.RegisterChild(new UIImage("Loading", horizontalMiddle: true, verticalMiddle: true, scale: 2, frame: 26, frameMaxTime: 8));
            column.RegisterChild(new Space(1, 20));
            column.RegisterChild(new UIText("SSFont", text: Main.GetText("save_bottom"), fontColor: Color.White, fontSize: 30, textHorizontalMiddle: true));
            column.HorizontalMiddle = true;
            column.VerticalMiddle = true;

            loading.RegisterState("loading_3", column);

            loading.BackgroundColor = new(6, 0, 16);

            loading.SelectChildById<SizeContainer>("state").Alpha = 0;

            var loadingTimer = new Timer((sender, args) =>
            {
                if (loading.CurrentState == "MainMenu") return;
                var timer = sender as Timer;
                var state = loading.SelectChildById<SizeContainer>("state");
                if (timer[0] > 0)
                {
                    if (state.Alpha < 1)
                        state.Alpha += 0.015f;
                    else
                        timer[0] -= 0.1f;
                }
                else state.Alpha -= 0.015f;
                if (state.Alpha < 0)
                {
                    state.Alpha = 0;
                    timer[0] = 10f;
                    var page = int.Parse(loading.CurrentState.Replace("loading_", ""));
                    loading.SwitchToState($"loading_{++page}");
                }
            });

            loadingTimer.timer[0] = 10;

            loadingTimer.id = "timer";

            loading.RegisterChild(loadingTimer);

            loading.SwitchToState("loading_0");

            Components.Add(loading);
        }
    }
}