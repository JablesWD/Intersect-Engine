﻿using System;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects.Maps.MapList;

namespace Intersect.Client.UI.Game
{
    class AdminWindow
    {
        private ComboBox mAccessDropdown;

        private Label mAccessLabel;

        //Controls
        private WindowControl mAdminWindow;

        private Button mBanButton;

        //Windows
        BanMuteBox mBanMuteWindow;

        private CheckBox mChkChronological;
        private ComboBox mFaceDropdown;
        private Label mFaceLabel;

        //Graphics
        public ImagePanel FacePanel;

        //Player Mod Buttons
        private Button mKickButton;

        private Button mKillButton;
        private Label mLblChronological;

        private TreeControl mMapList;
        private Label mMapListLabel;
        private Button mMuteButton;

        //Player Mod Labels
        private Label mNameLabel;

        //Player Mod Textboxes
        private TextBox mNameTextbox;

        private Button mSetFaceButton;
        private Button mSetPowerButton;
        private Button mSetSpriteButton;
        public ImagePanel SpriteContainer;
        private ComboBox mSpriteDropdown;
        private Label mSpriteLabel;
        public ImagePanel SpritePanel;
        private Button mUnbanButton;
        private Button mUnmuteButton;
        private Button mWarpMeToButton;
        private Button mWarpToMeButton;

        //Init
        public AdminWindow(Canvas gameCanvas)
        {
            mAdminWindow = new WindowControl(gameCanvas, Strings.Admin.title);
            mAdminWindow.SetSize(200, 540);
            mAdminWindow.SetPosition(GameGraphics.Renderer.GetScreenWidth() / 2 - mAdminWindow.Width / 2,
                GameGraphics.Renderer.GetScreenHeight() / 2 - mAdminWindow.Height / 2);
            mAdminWindow.DisableResizing();
            mAdminWindow.Margin = Margin.Zero;
            mAdminWindow.Padding = Padding.Zero;

            //Player Mods
            mNameLabel = new Label(mAdminWindow);
            mNameLabel.SetPosition(6, 4);
            mNameLabel.Text = Strings.Admin.name;

            mNameTextbox = new TextBox(mAdminWindow);
            mNameTextbox.SetBounds(6, 22, 188, 18);
            Gui.FocusElements.Add(mNameTextbox);

            mWarpToMeButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.warp2me
            };
            mWarpToMeButton.SetBounds(6, 44, 80, 18);
            mWarpToMeButton.Clicked += _warpToMeButton_Clicked;

            mWarpMeToButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.warpme2
            };
            mWarpMeToButton.SetBounds(6, 64, 80, 18);
            mWarpMeToButton.Clicked += _warpMeToButton_Clicked;

            mKickButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.kick
            };
            mKickButton.SetBounds(90, 44, 50, 18);
            mKickButton.Clicked += _kickButton_Clicked;

            mKillButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.kill
            };
            mKillButton.SetBounds(144, 44, 50, 18);
            mKillButton.Clicked += _killButton_Clicked;

            mBanButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.ban
            };
            mBanButton.SetBounds(90, 64, 50, 18);
            mBanButton.Clicked += _banButton_Clicked;

            mUnbanButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.unban
            };
            mUnbanButton.SetBounds(90, 84, 50, 18);
            mUnbanButton.Clicked += _unbanButton_Clicked;

            mMuteButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.mute
            };
            mMuteButton.SetBounds(144, 64, 50, 18);
            mMuteButton.Clicked += _muteButton_Clicked;

            mUnmuteButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.unmute
            };
            mUnmuteButton.SetBounds(144, 84, 50, 18);
            mUnmuteButton.Clicked += _unmuteButton_Clicked;

            mSpriteLabel = new Label(mAdminWindow);
            mSpriteLabel.SetPosition(6, 112);
            mSpriteLabel.Text = Strings.Admin.sprite;

            mSpriteDropdown = new ComboBox(mAdminWindow);
            mSpriteDropdown.SetBounds(6, 128, 80, 18);
            mSpriteDropdown.AddItem(Strings.Admin.none);
            var sprites =
                Globals.ContentManager.GetTextureNames(
                    GameContentManager.TextureType.Entity);
            Array.Sort(sprites, new AlphanumComparatorFast());
            foreach (var sprite in sprites)
            {
                mSpriteDropdown.AddItem(sprite);
            }
            mSpriteDropdown.ItemSelected += _spriteDropdown_ItemSelected;

            mSetSpriteButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.setsprite
            };
            mSetSpriteButton.SetBounds(6, 148, 80, 18);
            mSetSpriteButton.Clicked += _setSpriteButton_Clicked;

            SpriteContainer = new ImagePanel(mAdminWindow);
            SpriteContainer.SetSize(50, 50);
            SpriteContainer.SetPosition(115, 114);
            SpritePanel = new ImagePanel(SpriteContainer);

            mFaceLabel = new Label(mAdminWindow);
            mFaceLabel.SetPosition(6, 172);
            mFaceLabel.Text = Strings.Admin.face;

            mFaceDropdown = new ComboBox(mAdminWindow);
            mFaceDropdown.SetBounds(6, 188, 80, 18);
            mFaceDropdown.AddItem(Strings.Admin.none);
            var faces =
                Globals.ContentManager.GetTextureNames(
                    GameContentManager.TextureType.Face);
            Array.Sort(faces, new AlphanumComparatorFast());
            foreach (var face in faces)
            {
                mFaceDropdown.AddItem(face);
            }
            mFaceDropdown.ItemSelected += _faceDropdown_ItemSelected;

            mSetFaceButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.setface
            };
            mSetFaceButton.SetBounds(6, 208, 80, 18);
            mSetFaceButton.Clicked += _setFaceButton_Clicked;

            FacePanel = new ImagePanel(mAdminWindow);
            FacePanel.SetSize(50, 50);
            FacePanel.SetPosition(115, 174);

            mAccessLabel = new Label(mAdminWindow);
            mAccessLabel.SetPosition(6, 232);
            mAccessLabel.Text = Strings.Admin.access;

            mAccessDropdown = new ComboBox(mAdminWindow);
            mAccessDropdown.SetBounds(6, 248, 80, 18);
            mAccessDropdown.AddItem(Strings.Admin.access0).UserData = "None";
            mAccessDropdown.AddItem(Strings.Admin.access1).UserData = "Moderator";
            mAccessDropdown.AddItem(Strings.Admin.access2).UserData = "Admin";

            mSetPowerButton = new Button(mAdminWindow)
            {
                Text = Strings.Admin.setpower
            };
            mSetPowerButton.SetBounds(6, 268, 80, 18);
            mSetPowerButton.Clicked += _setPowerButton_Clicked;

            CreateMapList();
            mMapListLabel = new Label(mAdminWindow)
            {
                Text = Strings.Admin.maplist
            };
            mMapListLabel.SetPosition(4f, 294);

            mChkChronological = new CheckBox(mAdminWindow);
            mChkChronological.SetToolTipText(Strings.Admin.chronologicaltip);
            mChkChronological.SetPosition(mAdminWindow.Width - 24, 294);
            mChkChronological.CheckChanged += _chkChronological_CheckChanged;

            mLblChronological = new Label(mAdminWindow)
            {
                Text = Strings.Admin.chronological
            };
            mLblChronological.SetPosition(mChkChronological.X - 30, 294);

            UpdateMapList();
        }

        private void _spriteDropdown_ItemSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            SpritePanel.Texture =
                Globals.ContentManager.GetTexture(
                    GameContentManager.TextureType.Entity, mSpriteDropdown.Text);
            if (SpritePanel.Texture != null)
            {
                SpritePanel.SetUv(0, 0, .25f, .25f);
                SpritePanel.SetSize(SpritePanel.Texture.GetWidth() / 4, SpritePanel.Texture.GetHeight() / 4);
                Align.AlignTop(SpritePanel);
                Align.CenterHorizontally(SpritePanel);
            }
        }

        private void _faceDropdown_ItemSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            FacePanel.Texture =
                Globals.ContentManager.GetTexture(
                    GameContentManager.TextureType.Face, mFaceDropdown.Text);
        }

        //Methods
        public void SetName(string name)
        {
            mNameTextbox.Text = name;
        }

        private void CreateMapList()
        {
            mMapList = new TreeControl(mAdminWindow);
            mMapList.SetPosition(4f, 316);
            mMapList.Height = 188;
            mMapList.Width = mAdminWindow.Width - 8;
            mMapList.MaximumSize = new Framework.GenericClasses.Point(4096, 999999);
        }

        public void UpdateMapList()
        {
            mMapList.Dispose();
            CreateMapList();
            AddMapListToTree(MapList.GetList(), null);
        }

        private void AddMapListToTree(MapList mapList, TreeNode parent)
        {
            TreeNode tmpNode;
            if (mChkChronological.IsChecked)
            {
                for (int i = 0; i < MapList.GetOrderedMaps().Count; i++)
                {
                    tmpNode = mMapList.AddNode(MapList.GetOrderedMaps()[i].Name);
                    tmpNode.UserData = (MapList.GetOrderedMaps()[i]).MapId;
                    tmpNode.DoubleClicked += tmpNode_DoubleClicked;
                    tmpNode.Clicked += tmpNode_DoubleClicked;
                }
            }
            else
            {
                for (int i = 0; i < mapList.Items.Count; i++)
                {
                    if (mapList.Items[i].GetType() == typeof(MapListFolder))
                    {
                        if (parent == null)
                        {
                            tmpNode = mMapList.AddNode(mapList.Items[i].Name);
                            tmpNode.UserData = ((MapListFolder) mapList.Items[i]);
                            AddMapListToTree(((MapListFolder) mapList.Items[i]).Children, tmpNode);
                        }
                        else
                        {
                            tmpNode = parent.AddNode(mapList.Items[i].Name);
                            tmpNode.UserData = ((MapListFolder) mapList.Items[i]);
                            AddMapListToTree(((MapListFolder) mapList.Items[i]).Children, tmpNode);
                        }
                    }
                    else
                    {
                        if (parent == null)
                        {
                            tmpNode = mMapList.AddNode(mapList.Items[i].Name);
                            tmpNode.UserData = ((MapListMap) mapList.Items[i]).MapId;
                            tmpNode.DoubleClicked += tmpNode_DoubleClicked;
                            tmpNode.Clicked += tmpNode_DoubleClicked;
                        }
                        else
                        {
                            tmpNode = parent.AddNode(mapList.Items[i].Name);
                            tmpNode.UserData = ((MapListMap) mapList.Items[i]).MapId;
                            tmpNode.DoubleClicked += tmpNode_DoubleClicked;
                            tmpNode.Clicked += tmpNode_DoubleClicked;
                        }
                    }
                }
            }
        }

        void _kickButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                PacketSender.SendAdminAction((int) AdminActions.Kick, mNameTextbox.Text);
            }
        }

        void _killButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                PacketSender.SendAdminAction((int) AdminActions.Kill, mNameTextbox.Text);
            }
        }

        void _warpToMeButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                PacketSender.SendAdminAction((int) AdminActions.WarpToMe, mNameTextbox.Text);
            }
        }

        void _warpMeToButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                PacketSender.SendAdminAction((int) AdminActions.WarpMeTo, mNameTextbox.Text);
            }
        }

        void _muteButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                mBanMuteWindow = new BanMuteBox(Strings.Admin.mutecaption.ToString(mNameTextbox.Text),
                    Strings.Admin.muteprompt.ToString( mNameTextbox.Text), true, MuteUser);
            }
        }

        void MuteUser(object sender, EventArgs e)
        {
            PacketSender.SendAdminAction((int) AdminActions.Mute, mNameTextbox.Text,
                mBanMuteWindow.GetDuration().ToString(), mBanMuteWindow.GetReason(),
                Convert.ToString(mBanMuteWindow.BanIp()));
            mBanMuteWindow.Dispose();
        }

        void BanUser(object sender, EventArgs e)
        {
            PacketSender.SendAdminAction((int) AdminActions.Ban, mNameTextbox.Text,
                mBanMuteWindow.GetDuration().ToString(), mBanMuteWindow.GetReason(),
                Convert.ToString(mBanMuteWindow.BanIp()));
            mBanMuteWindow.Dispose();
        }

        void _banButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0 &&
                mNameTextbox.Text.Trim().ToLower() != Globals.Me.Name.Trim().ToLower())
            {
                mBanMuteWindow = new BanMuteBox(Strings.Admin.bancaption.ToString( mNameTextbox.Text),
                    Strings.Admin.banprompt.ToString( mNameTextbox.Text), true, BanUser);
            }
        }

        private void _setFaceButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                PacketSender.SendAdminAction((int) AdminActions.SetFace, mNameTextbox.Text, mFaceDropdown.Text);
            }
        }

        void _unmuteButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                var confirmWindow = new InputBox(Strings.Admin.unmutecaption.ToString( mNameTextbox.Text),
                    Strings.Admin.unmuteprompt.ToString( mNameTextbox.Text), true, InputBox.InputType.YesNo, UnmuteUser,
                    null, -1);
            }
        }

        void _unbanButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                var confirmWindow = new InputBox(Strings.Admin.unbancaption.ToString( mNameTextbox.Text),
                    Strings.Admin.unbanprompt.ToString( mNameTextbox.Text), true, InputBox.InputType.YesNo, UnbanUser,
                    null, -1);
            }
        }

        void UnmuteUser(object sender, EventArgs e)
        {
            PacketSender.SendAdminAction((int) AdminActions.UnMute, mNameTextbox.Text);
        }

        void UnbanUser(object sender, EventArgs e)
        {
            PacketSender.SendAdminAction((int) AdminActions.UnBan, mNameTextbox.Text);
        }

        void _setSpriteButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                PacketSender.SendAdminAction((int) AdminActions.SetSprite, mNameTextbox.Text, mSpriteDropdown.Text);
            }
        }

        void _setPowerButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mNameTextbox.Text.Trim().Length > 0)
            {
                PacketSender.SendAdminAction((int) AdminActions.SetAccess, mNameTextbox.Text,
                    mAccessDropdown.SelectedItem.UserData.ToString());
            }
        }

        void _chkChronological_CheckChanged(Base sender, EventArgs arguments)
        {
            UpdateMapList();
        }

        void tmpNode_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendAdminAction((int) AdminActions.WarpTo,"","","","",(Guid)((TreeNode) sender).UserData);
        }

        public void Update()
        {
        }

        public void Show()
        {
            mAdminWindow.IsHidden = false;
        }

        public bool IsVisible()
        {
            return !mAdminWindow.IsHidden;
        }

        public void Hide()
        {
            mAdminWindow.IsHidden = true;
        }
    }
}