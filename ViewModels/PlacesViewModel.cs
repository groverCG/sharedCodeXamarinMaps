using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedCode.Models;
using SharedCode.Services;

namespace SharedCode.ViewModels
{
	public class PlacesViewModel : ObservableObject
	{
		private readonly IPlaceRepository repository;
		private List<Place> places;
		private Place currentPlace;
		public int CurrentPlaceIndex;
		private bool isLoading;

		public Place CurrentPlace
		{
			get => currentPlace;
			set => SetProperty(ref currentPlace, value);
		}

		public List<Place> Places
		{
			get => places;
			set => SetProperty(ref places, value);
		}

		public bool IsLoading
		{
			get => isLoading;
			set => SetProperty(ref isLoading, value);
		}

		public ICommand LoadPlacesCommand { get; }

		public PlacesViewModel(IPlaceRepository repo)
		{
			this.repository = repo;
			LoadPlacesCommand = new RelayCommand(LoadPlaces);
		}

		private async void LoadPlaces()
		{
			IsLoading = true;
			try
			{
				Places = await repository.GetPlaces();
				CurrentPlace = Places[0];
			}
			finally
			{
				IsLoading = false;
			}
		}
	}
}

