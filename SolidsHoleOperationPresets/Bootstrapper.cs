﻿// <copyright file="Bootstrapper.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SHOP
{
    using System.Windows;

    using MahApps.Metro.Controls.Dialogs;

    using Microsoft.Practices.Unity;

    using Prism.Unity;

    using SHOP.Services;
    using SHOP.ViewModels;
    using SHOP.Views;

    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary> The window. </summary>
        private ShellView window;

        protected override DependencyObject CreateShell()
        {
            this.window = this.Container.Resolve<ShellView>();
            this.window.DataContext = this.Container.Resolve<ShellViewViewModel>();
            return this.window;
        }

        /// <summary> Initializes the shell. </summary>
        protected override void InitializeShell() => this.window.ShowDialog();

        /// <summary> Configures the <see cref="T:Microsoft.Practices.Unity.IUnityContainer" />
        ///           . May be overwritten in a derived class to add specific
        ///           type mappings required by the application. </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterType<ISerializationService,
                SerializationService>(new ContainerControlledLifetimeManager());

            this.Container.RegisterType<IFileBrowserService,
                FileBrowserService>(new ContainerControlledLifetimeManager());

            this.Container.RegisterType<ISerializationManager,
                SerializationManager>(new ContainerControlledLifetimeManager());

            this.Container.RegisterType<IDialogCoordinator,
                DialogCoordinator>(new ContainerControlledLifetimeManager());
        }
    }
}