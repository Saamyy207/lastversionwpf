   M S B A M L   `   `   `  ����  GestionaireEtudiant    GestionaireEtudiant.Stats   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^Syncfusion.SfChart.WPF, Version=28.1462.38.0, Culture=neutral, PublicKeyToken=3d67ed1f87d44c892
syncfusion!http://schemas.syncfusion.com/wpf  6K     � 	Resources  5	       
  Color1 
 Color2 
 Color3��%{   &	        &	      &	 >     ��5
   
   #FF5733��6(   ��5   
   #33FF57��6(   ��5   
   #3357FF��6(   5      .��5      � 
�������.+�5   
   )  #Syncfusion.UI.Xaml.Charts.SfChart3D     PrimaryAxis 5      .  (Syncfusion.UI.Xaml.Charts.CategoryAxis3D  5      )  #Syncfusion.UI.Xaml.Charts.ChartAxis   Header$ Filière��6,      SecondaryAxis 5      /  )Syncfusion.UI.Xaml.Charts.NumericalAxis3D  5      $ Registrations��6-      Series. 5      .  (Syncfusion.UI.Xaml.Charts.ColumnSeries3D  /  )Syncfusion.UI.Xaml.Charts.ChartSeriesBase   XBindingPath$ Field��5   !   .  (Syncfusion.UI.Xaml.Charts.XyDataSeries3D   YBindingPath$ RegistrationCount��5   !      Label$ Field��5   !      ItemsSource 5   (   �� *Data+5      5      5!      5#      