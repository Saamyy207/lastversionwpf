   M S B A M L   `   `   `  ����N  JGestionaireEtudiant, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GestionaireEtudiantGestionaireEtudiant  5   	   $    GestionaireEtudiant.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:GestionaireEtudiant     5   	     � Title$  
MainWindow��5   	    � WindowState$ 	Maximized=�5	   
   $
��1500��5
   	   $	��500��5   	    � 	Resources 5         TabButtonStyle �� 
TargetType �� Property �� Value �� Color �� 
TargetType �� Padding	 �� Triggers
 P� Property P� Value N� EnterActions �� TargetProperty	 �� To g� Duration N� ExitActions��%.  &	        �� 5   
   " ��6'   .�5      ��  � ��
 �����6+   �� 5       �@�OverridesDefaultStyle . 66   �� 5       � ��$ 13(�6)   �� 5       � ��$ 0q�60   �� 5       � �� 5      �� 5      $ Transparent��6&   5      �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 10 15 10 15q�6(   �� 5       � ��$ Stretch=�64   �� 5       � ��$ Segoe UI)�6+   �� 5       � �� 5      �� 5      " ��6&   .R�5       �� #��z��6!   $ 
10 20 0 20q�6K   ._�5!      �� $
��Left=�6/   $��Center=�6J   5"      5#      5%      	 5&      P� 5'      
 �@J�IsMouseOver .  5(      �� 5)      .��5*      �� .��5+   "   �� $1 +(Button.Background).(SolidColorBrush.Color)�5,   %   $ #90E0EFX�5-   %   $ 0:0:0.3D�5.   %   5/      50       52      �� 53      .��54      �� .��55   "   �� $* $(Background).(SolidColorBrush.Color)�61   $ TransparentX�56   1   $ 0:0:0.3D�6B   57      58      5:      5<      5=      .��5?      � .+�5@   
   ]� .+�5A      �� $
��Left=�6   $��Stretch=�6/    �� Orientation$ Vertical=�6K   $	��180��6b   ���#0077B6.+�5C      �� -   5        #��[  5C      $��Stretch=�6V   .��5D      �� $ 
Horizontal=�6!   .+�5E      �� $��./icons/student.png��6    $��20��6=   $��20��6H   �� 5F      $��Gestion des étudiants��6$   $��Center=�6A   $
��Left=�6\   $��5,0,0,0q�6w   5G      5H      �� 5J      -   5        #��[  5J      $��Stretch=�6B   .��5K      �� $ 
Horizontal=�6!   .+�5L      �� $��./icons/ff.png��6    $��20��68   $��20��6C   �� 5M      $��Gestion des Filières��6$   $��Center=�6@   $
��Left=�6[   $��5,0,0,0q�6v   5N      5O      �� 5Q      -   5        #��[  5Q      $��Stretch=�6B   .��5R      �� $ 
Horizontal=�6!   .+�5S      �� $��./icons/statistics.png��6    $��20��6@   $��20��6K   �� 5T      $��Statistiques��6$   $��Center=�68   $
��Left=�6S   $��5,0,0,0q�6n   5U      5V      �� 5X      -   5        #��[  5X      $��Stretch=�6C   .��5Y      �� $ 
Horizontal=�6"   .+�5Z      �� $��./icons/deconnecter.png��6    $��20��6A   $��20��6L   �� 5[      $��Se déconnecter��6$   $��Center=�6:   $
��Left=�6U   $��5,0,0,0q�6p   5\      5]      5^      �� 5`      -   5         ��Name$ MainContent��5`      5b      �� 5e      -   5        $ 
TabContent��5e      $��Right=�61   5f      5g      5h      