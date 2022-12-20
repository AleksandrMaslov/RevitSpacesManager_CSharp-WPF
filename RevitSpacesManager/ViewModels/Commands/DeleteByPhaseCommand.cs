﻿using RevitSpacesManager.Models;
using System.Windows;

namespace RevitSpacesManager.ViewModels
{
    internal class DeleteByPhaseCommand : Command
    {
        internal IDeleting Model { get; set; }

        private readonly MainWindowViewModel _viewModel;


        public DeleteByPhaseCommand(MainWindowViewModel mainWindowViewModel, IDeleting model)
        {
            _viewModel = mainWindowViewModel;
            Model = model;
        }


        public override bool CanExecute(object parameter) => true;
        public override void Execute(object parameter)
        {
            if (_viewModel.IsNothingToDelete())
            {
                _viewModel.ShowNothingDeleteMessage();
                return;
            }

            if (_viewModel.IsCurrentPhaseNotSelected())
            {
                _viewModel.ShowPhaseNotSelectedMessage();
                return;
            }

            if (Model.AreNotAllPhaseElementsEditable(_viewModel.CurrentDocumentPhaseSelected))
            {
                _viewModel.ShowNoAccessMessage();
                return;
            }

            MessageGenerator messageGenerator = new MessageGenerator(
                _viewModel.ActiveObject,
                _viewModel.GetCurrentSelectedPhaseNumberOfElements(),
                _viewModel.CurrentDocumentPhaseSelected
            );

            if (_viewModel.ShowConfirmationDialog(messageGenerator.MessageDeleteSelected()) == MessageBoxResult.Cancel)
            {
                return;
            }

            Model.DeleteByPhase(_viewModel.CurrentDocumentPhaseSelected);

            _viewModel.ShowReportMessage(messageGenerator.ReportDeleteSelected());
            _viewModel.OnPropertyChanged(nameof(_viewModel.CurrentDocumentPhases));
        }
    }
}