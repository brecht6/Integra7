using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Integra7AuralAlchemist.ViewModels;
using ReactiveUI;
using System;

namespace Integra7AuralAlchemist.Views;

public partial class SaveUserToneDialog : ReactiveWindow<SaveUserToneViewModel>
{
    public SaveUserToneDialog()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;
        
        this.WhenActivated(action => 
            action(ViewModel!.CancelCommand.Subscribe(Close)));
    }
}