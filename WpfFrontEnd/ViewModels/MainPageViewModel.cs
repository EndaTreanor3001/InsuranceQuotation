using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppliedSystems;
using AppliedSystems.Dtos;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.Interfaces;
using AppliedSystems.Providers;
using AppliedSystems.Transforms;

namespace WpfFrontEnd.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _driverOccupation;
        private string _driverName;
        private DateTime _policyStartDate = DateTime.Today;
        private DateTime _driversDateOfBirth = DateTime.Today;
        private IList<string> _addedDrivers = new List<string>();
        private bool _showClaimsStackPanel;
        private DateTime? _claimDate1;
        private IList<IDriver> _policyDrivers = new List<IDriver>();
        private IList<IClaim> _claims = new List<IClaim>();
        private static readonly IProvide<DateTime> _todayProvider = new TodayProvider();
        private static readonly RejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform _rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform = new RejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform();
        private static readonly RejectionMessageAndSuccessBasedOnYoungestDriverTransform _rejectionMessageAndSuccessBasedOnYoungestDriverTransform = new RejectionMessageAndSuccessBasedOnYoungestDriverTransform(Properties.Settings.Default.YoungestDriverAge, _todayProvider);
        private static readonly RejectionMessageAndSuccessBasedOnOldestDriverTransform _rejectionMessageAndSuccessBasedOnOldestDriverTransform = new RejectionMessageAndSuccessBasedOnOldestDriverTransform(75, _todayProvider);
        private static readonly IEnumerable<ITransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>> _driverBasedRejectionMessageAndSuccessTransforms = new ITransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>[] { _rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform, _rejectionMessageAndSuccessBasedOnYoungestDriverTransform, _rejectionMessageAndSuccessBasedOnOldestDriverTransform  };
        private static readonly RejectionMessageAndSuccessBasedOnPolicyStartDateTransform _rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = new RejectionMessageAndSuccessBasedOnPolicyStartDateTransform(_todayProvider);
        private static readonly DriversAndPremiumToUpdatedPremiumForYoungDrivers _driversAndPremiumToUpdatedPremiumForYoungDrivers = new DriversAndPremiumToUpdatedPremiumForYoungDrivers(Properties.Settings.Default.StartingYoungAge, Properties.Settings.Default.EndingYoungAge, _todayProvider, Properties.Settings.Default.IncreasePremiumValue);
        private static readonly DriversAndPremiumToUpdatedPremiumForAdultDrivers _driversAndPremiumToUpdatedPremiumForAdultDrivers = new DriversAndPremiumToUpdatedPremiumForAdultDrivers(Properties.Settings.Default.StartingAdultAge, Properties.Settings.Default.OldestDriverAge, _todayProvider, Properties.Settings.Default.DecreasePremiumValue);
        private static readonly DriversAndPremiumToUpdatedPremiumBasedOnAgeTransform _driversAndPremiumToUpdatedPremiumBasedOnAgeTransform = new DriversAndPremiumToUpdatedPremiumBasedOnAgeTransform(_driversAndPremiumToUpdatedPremiumForYoungDrivers , _driversAndPremiumToUpdatedPremiumForAdultDrivers);
        private static readonly DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransform _driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform = new DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransform(Properties.Settings.Default.OccupationsWherePremiumIsDecreased.Split(','), Properties.Settings.Default.DecreaseAmountBasedOnOccupation);
        private static readonly DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransform _driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform = new DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransform(Properties.Settings.Default.OccupationsWherePremiumIsIncreased.Split(',') ,Properties.Settings.Default.IncreaseAmountBasedOnOccupation);
        private static readonly DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransform _driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform = new DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransform(_driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform, _driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform );
        private static readonly DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform _driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform = new DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(Properties.Settings.Default.SmallerPercentageIncrease, Properties.Settings.Default.LargerPercentageIncrease, Properties.Settings.Default.SmallerTimeSpanInYears, Properties.Settings.Default.LargerTimeSpanInYears, _todayProvider);
        private static readonly PolicyToRejectionMessageAndSuccess _policyToRejectionMessageAndSuccess = new PolicyToRejectionMessageAndSuccess(_rejectionMessageAndSuccessBasedOnPolicyStartDateTransform, _driverBasedRejectionMessageAndSuccessTransforms );
        private readonly PolicyToResultTransform _policyToResultTransform = new PolicyToResultTransform(Properties.Settings.Default.StartingPremiumAmount, _policyToRejectionMessageAndSuccess, _driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform, _driversAndPremiumToUpdatedPremiumBasedOnAgeTransform, _driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform  );
        private string _policyQuoation;
        private bool _policyHasDrivers;
        private bool _haveQuotation;
        private DateTime? _claimDate2;
        private DateTime? _claimDate3;
        private DateTime? _claimDate4;
        private DateTime? _claimDate5;
        private bool _policyStartDateIsBeforeToday;
        private bool _canAddDriver;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            Occupations = Properties.Settings.Default.Occupations.Split(',');
            DriversDateOfBirth = DateTime.Today;
            PolicyStartDate = DateTime.Today;
            DriverName = Constants._ENTER_YOUR_NAME_MESSAGE;
        }

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public DateTime PolicyStartDate
        {
            get { return _policyStartDate;}

            set
            {
                _policyStartDate = value;
                PolicyStartDateIsBeforeToday = _policyStartDate < DateTime.Today;
                CanAddDriver = DriverName != Constants._ENTER_YOUR_NAME_MESSAGE && !string.IsNullOrWhiteSpace(DriverName) && !PolicyStartDateIsBeforeToday;
                OnPropertyChanged("PolicyStartDate");
            }
        }

        public DateTime DriversDateOfBirth
        {
            get { return _driversDateOfBirth;}

            set
            {
                _driversDateOfBirth = value;
                OnPropertyChanged("PolicyStartDate");
            }
        }

        public string DriverOccupation
        {
            get { return _driverOccupation;}

            set
            {
                _driverOccupation = value;
                OnPropertyChanged("DriverOccupation");
            }
        }

        public string DriverName
        {
            get { return _driverName;}

            set
            {
                _driverName = value;
                CanAddDriver = DriverName != Constants._ENTER_YOUR_NAME_MESSAGE && !string.IsNullOrWhiteSpace(DriverName) && !PolicyStartDateIsBeforeToday;
                OnPropertyChanged("DriverName");
            }
        }

        public IEnumerable<string> Occupations { get; set; }

        public IList<string> AddedDrivers
        {
            get { return _addedDrivers; }
            set
            {
                OnPropertyChanged("AddedDrivers");
                _addedDrivers = value;
            }
        }

        public ICommand GetEstimationCommand
        {
            get { return new DelegateCommand(GetPolicyEstimation, () => PolicyHasDrivers); }
        }

        public bool PolicyHasDrivers
        {
            get { return _policyHasDrivers; }
            set
            {
                _policyHasDrivers = value;
                OnPropertyChanged("PolicyHasDrivers");
            }
        }

        public ICommand AddClaimsCommand
        {
            get { return new DelegateCommand( ShowClaims, () => true);}
        }

        private void ShowClaims()
        {
            ShowClaimsStackPanel = true;
        }

        private void GetPolicyEstimation()
        {
            PolicyQuoation =  _policyToResultTransform.Transform(new Policy(PolicyStartDate, PolicyDrivers));
            PolicyHasDrivers = false;
            HaveQuotation = true;
        }

        public bool HaveQuotation
        {
            get { return _haveQuotation; }
            set
            {
                _haveQuotation = value;
                OnPropertyChanged("HaveQuotation");
            }
        }

        public string PolicyQuoation
        {
            get { return _policyQuoation; }
            set
            {
                _policyQuoation = value;
                OnPropertyChanged("PolicyQuoation");
            }
        }

        public bool PolicyStartDateIsBeforeToday
        {
            get { return  PolicyStartDate < DateTime.Today; }
            set
            {
                _policyStartDateIsBeforeToday = value;
                OnPropertyChanged("PolicyStartDateIsBeforeToday");
            }
        }

        public ICommand AddNewDriverCommand => new DelegateCommand(AddNewDriver, CanAddNewDriver);

        public ICommand ClearScreenCommand => new DelegateCommand(Reset, () => HaveQuotation);

        private void Reset()
        {
            PolicyDrivers.Clear();
            PolicyHasDrivers = false;
            HaveQuotation = false;
            PolicyStartDate = DateTime.Today;
            DriversDateOfBirth = DateTime.Today;

        }

        private void AddNewDriver()
        {
            if (CanAddNewDriver())
            {
                Claims = CollectClaims().ToList();
                var driversClaims = Claims;
                var driver = new Driver(DriverName, DriverOccupation, DriversDateOfBirth, driversClaims);
                _policyDrivers.Add(driver);
                DriverName = Constants._ENTER_YOUR_NAME_MESSAGE;
                DriverOccupation = "Other";
                DriversDateOfBirth = DateTime.Today;
                ShowClaimsStackPanel = false;
                PolicyHasDrivers = true;
                ClaimDate1 = null;
                ClaimDate2 = null;
                ClaimDate3 = null;
                ClaimDate4 = null;
                ClaimDate5 = null;
            }
            else
            {
                DriverName = "";
            }

        }

        private bool CanAddNewDriver()
        {
            return  DriverName != Constants._ENTER_YOUR_NAME_MESSAGE && !string.IsNullOrWhiteSpace(DriverName) && !PolicyStartDateIsBeforeToday;
        }

        public bool CanAddDriver
        {
            get { return _canAddDriver; }
            set
            {
                _canAddDriver = value;
                OnPropertyChanged("CanAddDriver");
            }
        }

        public IList<IDriver> PolicyDrivers
        {
            get { return _policyDrivers; }
            private set
            {
                _policyDrivers = value;
                OnPropertyChanged("PolicyDrivers");
            }
        }

        private IEnumerable<IClaim> CollectClaims()
        {
            if (ClaimDate1 != null)
            {
                yield return new Claim((DateTime)ClaimDate1);
            }
            if (ClaimDate2 != null)
            {
                yield return new Claim((DateTime)ClaimDate2);
            }
            if (ClaimDate3 != null)
            {
               yield return new Claim((DateTime)ClaimDate3);
            }
            if (ClaimDate4 != null)
            {
                yield return new Claim((DateTime)ClaimDate4);
            }
            if (ClaimDate5 != null)
            {
                yield return new Claim((DateTime)ClaimDate5);
            }
            
        }

        public IList<IClaim> Claims
        {
            get { return _claims; }
            set { _claims = value; }
        }

        public DateTime? ClaimDate1
        {
            get { return _claimDate1; }
            set
            {
                _claimDate1 = value;
                OnPropertyChanged("ClaimDate1");
            }
        }

        public DateTime? ClaimDate2
        {
            get { return _claimDate2; }
            set
            {
                _claimDate2 = value;
                OnPropertyChanged("ClaimDate2");
            }
        }

        public DateTime? ClaimDate3
        {
            get { return _claimDate3; }
            set
            {
                _claimDate3 = value;
                OnPropertyChanged("ClaimDate3");
            }
        }

        public DateTime? ClaimDate4
        {
            get { return _claimDate4; }
            set
            {
                _claimDate4 = value;
                OnPropertyChanged("ClaimDate4");
            }
        }

        public DateTime? ClaimDate5
        {
            get { return _claimDate5; }
            set
            {
                _claimDate5 = value;
                OnPropertyChanged("ClaimDate5");
            }
        }

        public bool ShowClaimsStackPanel
        {
            get { return _showClaimsStackPanel; }
            set
            {
                _showClaimsStackPanel = value;
                OnPropertyChanged("ShowClaimsStackPanel");
            }
        }
    }
}
