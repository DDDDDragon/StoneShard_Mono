using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneShard_Mono.Content.Components;
using System;

namespace StoneShard_Mono.Content.Scenes
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

            loading.RegisterState("loading_1", new UIImage("ISG_logo", horizontalMiddle: true, verticalMiddle: true));

            loading.RegisterState("loading_2", new UIImage("HT_logo", horizontalMiddle: true, verticalMiddle: true));

            #region 第三幕

            var column = new ColumnContainer();
            column.RegisterChild(new UIText("SSFont", text: Main.GetText("save_top"), fontColor: Color.White, fontSize: 30, textVerticalMiddle: true) { HorizontalMiddle = true });
            column.RegisterChild(new Space(1, 20));
            column.RegisterChild(new UIImage("Loading", horizontalMiddle: true, frame: 26, frameMaxTime: 8));
            column.RegisterChild(new Space(1, 20));
            column.RegisterChild(new UIText("SSFont", text: Main.GetText("save_bottom"), fontColor: Color.White, fontSize: 30, textHorizontalMiddle: true) { HorizontalMiddle = true });
            column.HorizontalMiddle = true;
            column.VerticalMiddle = true;

            loading.RegisterState("loading_3", column);

            #endregion

            #region 主菜单

            var size = new SizeContainer(Main.GameWidth, Main.GameHeight);

            size.RegisterChild(new UIImage("mainMenuBG", relativePos: new(-80, 250)) { id = "background" });

            size.RegisterChild(new UIImage("menuBlink", relativePos: new(82, 678), frame: 6, frameMaxTime: 10) { id = "blink", Alpha = 0.1f });

            size.RegisterChild(new UIImage("menuShard", relativePos: new(240, 706), frame: 30, frameMaxTime: 5) { id = "shard" });

            size.RegisterChild(new UIImage("menuSparkles", relativePos: new(180, 584), frame: 30, frameMaxTime: 5) { id = "sparkles" });

            size.RegisterChild(new UIImage("mainMenuParralax_1", relativePos: new(534, 696), frame: 6, frameMaxTime: 10) { id = "parralax_1" });

            size.RegisterChild(new UIImage("mainMenuParralax_2", relativePos: new(-46, 896), frame: 6, frameMaxTime: 10) { id = "parralax_2" });

            size.RegisterChild(new UIImage("menuVingette") { Alpha = 0.5f });

            size.RegisterChild(new UIImage("light", drawMode: BlendState.Additive) { Alpha = 0.47f, id = "light" });

            size.RegisterChild(new UIText("SSFont", text: Main.GetText("press_any"), fontSize: 30, fontColor: Color.White, relativePos: new(0, 1100)) { HorizontalMiddle = true, id = "press" });

            #region 按钮

            var menu = new ColumnContainer() { ChildrenMargin = 2, RelativePosition = new(1500, 300), id = "menu" };

            menu.RegisterChild(new UIImage("StoneShardLogo", frame: 10, frameMaxTime: 10, repeat: false));

            menu.RegisterChild(new Space(1, 16));

            menu.RegisterChild(new UIText("SSFont", text: "Beta 0.8.2.10") { HorizontalMiddle = true });

            menu.RegisterChild(new Space(1, 20));

            var menuState = new StateContainer(new(254, 800)) { HorizontalMiddle = true };

            #region menu_0

            var menu_0 = new ColumnContainer() { ChildrenMargin = 2 };

            menu_0.RegisterChild(new Button("menuButton", hoverID: "menuButtonHover", pressID: "menuButtonPress",
                text: Main.GetText("play"), fontSize: 25, click: (sender, args) => { menuState.SwitchToState("menu_play"); })
            { id = "play", HorizontalMiddle = true });

            menu_0.RegisterChild(new Button("menuButton", hoverID: "menuButtonHover", pressID: "menuButtonPress",
                text: Main.GetText("settings"), fontSize: 25)
            { id = "settings", HorizontalMiddle = true });

            menu_0.RegisterChild(new Button("menuButton", hoverID: "menuButtonHover", pressID: "menuButtonPress",
                text: Main.GetText("credits"), fontSize: 25)
            { id = "credits", HorizontalMiddle = true });

            menu_0.RegisterChild(new Button("menuButton", hoverID: "menuButtonHover", pressID: "menuButtonPress",
                text: Main.GetText("exit"), fontSize: 25)
            { id = "exit", HorizontalMiddle = true });

            menuState.RegisterState("menu_0", menu_0);

            menuState.SwitchToState("menu_0");

            #endregion

            #region menu_play

            var menu_play = new ColumnContainer() { ChildrenMargin = 2 };

            menu_play.RegisterChild(new Button("menuButton", hoverID: "menuButtonHover", pressID: "menuButtonPress",
                text: Main.GetText("new_game"), fontSize: 25, click: (sender, args) => { menuState.SwitchToState("menu_new_game"); })
            { id = "newGame", HorizontalMiddle = true });

            //如果有存档添加一个load按钮

            menu_play.RegisterChild(new Space(10, 20));

            menu_play.RegisterChild(new Button("menuButton", hoverID: "menuButtonHover", pressID: "menuButtonPress",
                text: Main.GetText("back"), fontSize: 25, click: (sender, args) => { menuState.SwitchToState("menu_0"); })
            { id = "back", HorizontalMiddle = true });

            menuState.RegisterState("menu_play", menu_play);

            #endregion

            #region menu_new_game

            var menu_new_game = new ColumnContainer() { ChildrenMargin = 2 };

            menu_new_game.RegisterChild(new Button("menuButton", hoverID: "menuButtonHover", pressID: "menuButtonPress",
                text: Main.GetText("adventure"), fontSize: 25)
            { id = "newGame", HorizontalMiddle = true });

            //如果有存档添加一个load按钮

            menu_new_game.RegisterChild(new Space(10, 20));

            menu_new_game.RegisterChild(new Button("menuButton", hoverID: "menuButtonHover", pressID: "menuButtonPress",
                text: Main.GetText("back"), fontSize: 25, click: (sender, args) => { menuState.SwitchToState("menu_play"); })
            { id = "back", HorizontalMiddle = true });

            menuState.RegisterState("menu_new_game", menu_new_game);

            #endregion

            menu.RegisterChild(menuState);

            menu.Visible = false;

            size.RegisterChild(menu);

            #endregion

            size.RegisterChild(new Timer((sender, gameTime) =>
            {
                var timer = sender as Timer;
                timer[0]++;
                var size = timer.Parent as SizeContainer;

                var bg = size.SelectChildById("background");
                var shard = size.SelectChildById("shard");
                var sparkles = size.SelectChildById("sparkles");
                var light = size.SelectChildById("light");
                var blink = size.SelectChildById("blink");

                var parralax1 = size.SelectChildById("parralax_1");
                var parralax2 = size.SelectChildById("parralax_2");

                var press = size.SelectChildById("press");

                var menu = size.SelectChildById("menu");

                var offset = Mouse.GetState().Position.ToVector2() * 0.01f;

                bg.DrawOffset += offset;
                shard.DrawOffset += offset;
                sparkles.DrawOffset += offset;
                blink.DrawOffset += offset;

                light.DrawOffset += offset;
                light.DrawScale = (float)(Math.Sin(timer[0] / 15) * 0.7f + Main.Random.Next(10) / 1000 + 7);
                light.RelativePosition = shard.Rectangle.Center.ToVector2() - light.Rectangle.Size.ToVector2() / 2 * light.DrawScale;

                parralax1.DrawOffset += offset * 2;
                parralax2.DrawOffset += offset * 2;

                if (press.Visible)
                {
                    if (timer[1] == 0)
                    {
                        press.Alpha -= 0.012f;
                        if (press.Alpha < 0)
                            timer[1] = 1;
                    }
                    else
                    {
                        press.Alpha += 0.012f;
                        if (press.Alpha > 1)
                            timer[1] = 0;
                    }
                }

                var pressKeys = Keyboard.GetState().GetPressedKeys();

                if ((pressKeys.Length > 0 || size.Clicked) && press.Visible)
                {
                    press.Visible = false;
                    menu.Visible = true;
                }
            }));

            loading.RegisterState("MainMenu", size);

            #endregion

            #region 其他设置

            loading.BackgroundColor = GameColors.UIDark;

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
                    if (loading.CurrentState == "loading_3")
                    {
                        state.Alpha = 1;
                        loading.SwitchToState("MainMenu");
                        return;
                    }
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

            #endregion

            Components.Add(loading);
        }
    }
}