﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace Shipfer.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;
}
