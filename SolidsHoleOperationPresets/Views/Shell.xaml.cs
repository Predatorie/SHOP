// <copyright file="Shell.xaml.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SolidsHoleOperationPresets.Views
{
    using System.Windows;
    using MahApps.Metro.Controls.Dialogs;

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class Shell
    {
        public Shell() => this.InitializeComponent();

        /// <summary>
        /// If using DialogParticipation on Windows which open / close frequently you will get a
        /// memory leak unless you unregister.  The easiest way to do this is in your Closing/ Unloaded event
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Routed event information.</param>
        private void Shell_OnUnloaded(object sender, RoutedEventArgs e) => DialogParticipation.SetRegister(this, null);
    }
}