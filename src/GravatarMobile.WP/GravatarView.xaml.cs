﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GravatarMobile.Core.Interfaces;
using GravatarMobile.Core;
using GravatarMobile.Core.Data;
using System.Windows.Media;

namespace GravatarMobile.WP
{
    public partial class GravatarView : UserControl, IGravatarView
    {
        public static DependencyProperty IntitialStyleProperty = DependencyProperty.Register("IntitialStyle", typeof(Enums.GravatarViewStyle), typeof(GravatarView), null);

        #region Fields

        private Gravatar mItem;
        private Enums.GravatarViewStyle mStyle = Enums.GravatarViewStyle.Square;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Gravatar item.
        /// </summary>
        /// <value>The item.</value>
        public Gravatar Avatar
        {
            get
            {
                return mItem;
            }
            set
            {
                if (mItem != value)
                {
                    mItem = value;

                    LoadImageAysnc();
                }
            }
        }

        /// <summary>
        /// Get the initial style
        /// </summary>
        public Enums.GravatarViewStyle IntitialViewStyle
        {
            get
            {
                return (Enums.GravatarViewStyle)GetValue(IntitialStyleProperty);
            }
            set
            {
                SetValue(IntitialStyleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the view style.
        /// </summary>
        /// <value>The view style.</value>
        public Enums.GravatarViewStyle ViewStyle
        {
            get
            {
                return mStyle;
            }
            set
            {
                if (mStyle != value)
                {
                    mStyle = value;

                    UpdateStyle();
                }
            }
        }

        private void UpdateStyle()
        {
            if (mStyle == Enums.GravatarViewStyle.Round)
            {
                imgAvatar.Clip = new EllipseGeometry()
                {
                    Center = new Point(this.Width / 2, this.Height / 2),
                    RadiusX = this.Width / 2,
                    RadiusY = this.Height / 2,
                };
            }
            else
            {
                imgAvatar.Clip = null;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructors
        /// </summary>
        public GravatarView()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods

        private async void LoadImageAysnc()
        {
            var size = (this.Width > 80) ? this.Width : 80;

            await mItem.LoadImageAsync((int)size);

            this.Dispatcher.BeginInvoke(() =>
                {
                    imgAvatar.Source = mItem.ImageAsBitmap();

                });

        }

        #endregion

        #region Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            ViewStyle = IntitialViewStyle;
        }

        #endregion
    }
}
