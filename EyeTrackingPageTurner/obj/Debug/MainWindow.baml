   M S B A M L   `   `   `  ����P  LEyeTrackingPageTurner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:EyeTrackingPageTurnerEyeTrackingPageTurner  5   	   &     EyeTrackingPageTurner.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:EyeTrackingPageTurner     5   	   -   5          � Title$  Turner��5   	   $	��608��6   $	��800��6%   .��5	      � $��0,0,0,1q�6   .+�5
   
   �� $��Turner��6   $
��Left=�6!   $��	10,10,0,0q�6<   $	��Top=�6O   $��38��6g   $	��381��6s   $��20(�6   �� 5   
   $"��The eye tracking page turner��6   $
��Left=�67   $��	11,35,0,0q�6R   $	��Top=�6e   $	��319��6}   �� 5   
   $
��Left=�6   $��172,205,0,0q�6.   $	��Top=�6C   $	��120��6[   �� 5   
   $
��Left=�6   $��349,204,0,0q�6.   $	��Top=�6C   $	��120��6[   �� 5   
   $��	Left Side��6   $
��Left=�6$   $��172,165,0,0q�6?   $	��Top=�6T   $
��Bold"�6l   �� 5   
   $��
Right Side��6   $
��Left=�6%   $��349,164,0,0q�6@   $	��Top=�6U   $
��Bold"�6m   �� 5   
   $��Key to press��6   $
��Left=�6'   $��
80,201,0,0q�6B   $	��Top=�6V   $
��Bold"�6n   �� 5   
   $��Fixation Y Axis��6   $
��Left=�6*   $��
69,339,0,0q�6E   $	��Top=�6Y   $
��Bold"�6q   �� 5   
   -   5         ��Name$ textboxFixationYLeft��5      $
��Left=�60   $��23��6K   $��172,366,0,0q�6W    �� TextWrapping$
 Wrap=�6l   $��TextBox��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ textboxFixationYRight��5      $
��Left=�61   $��23��6L   $��349,366,0,0q�6X   $
 Wrap=�6m   $��TextBox��6�   $	��Top=�6�   $	��120��6�   �� 5   
   $��Status��6   $
��Left=�6!   $��
408,45,0,0q�6<   $	��Top=�6P   $
��Bold"�6h   �� 5   
   $��Fixation hold time (ms)��6   $
��Left=�62   $��
17,113,0,0q�6M   $	��Top=�6a   $
��Bold"�6y   �� 5   
   -   5        $ textBoxStatus��5      $
��Left=�6)   $��22��6D   $��
467,49,0,0q�6P   $
 Wrap=�6d   $��TextBox��6x   $	��Top=�6�   $	��297��6�   {�.  .��6�   5      �� 5   
   -   5        $ textBoxKeyPressStatus��5      $
��Left=�61   $��22��6L   $��
467,71,0,0q�6X   $
 Wrap=�6l   $��TextBox��6�   $	��Top=�6�   $	��120��6�   {�.  6�   �� 5   
   -   5        $ textBoxTimeStatus��5      $
��Left=�6-   $��22��6H   $��
587,71,0,0q�6T   $
 Wrap=�6h   $��TextBox��6|   $	��Top=�6�   $	��120��6�   {�.  6�   �� 5   
   -   5        $ textBoxHoldTimeMs��5      $
��Left=�6-   $��22��6H   $��172,117,0,0q�6T   $
 Wrap=�6i   $��TextBox��6}   $	��Top=�6�   $	��120��6�   �� 5   
   $
��Left=�6   $��172,342,0,0q�6,   $	��Top=�6A   $	��120��6Y   �� 5   
   $
��Left=�6   $��349,339,0,0q�6,   $	��Top=�6A   $	��120��6Y   �� 5   
   $��Width��6   $
��Left=�6    $��113,268,0,0q�6;   $	��Top=�6P   $
��Bold"�6h   �� 5    
   -   5         ��Name$ sliderLeftWidth��5       $
��Left=�5!      $��172,271,0,0q�5"      $	��Top=�5#      $	��120��5$       � Minimum$ 0Q�5&       � Maximum$ 15Q�5'       �� TickPlacement$ TopLeft=�5(       �� IsSnapToTickEnabled .  � Value 5%      �� *	LeftWidth+	 �� Mode$	 TwoWay=�5)      �� 5+   
   -	   5        
 ��Name$
 labelLeftWidth��5+      $
��Left=�5,      $��23��5-      $��172,295,0,0q�5.      $��Center=�5/      $	��Top=�51      $	��120��52      ��50      ��  �� ElementName$ sliderLeftWidth�� �� Path$ Value�52      �� 54   
   -
   5        $ sliderRightWidth��54      $
��Left=�55      $��349,268,0,0q�56      $	��Top=�57      $	��120��58      $ 0Q�5:      $ 15Q�5;      $ TopLeft=�5<       .  59      �� *
RightWidth+$	 TwoWay=�5=      �� 5?   
   -   5        $
 labelRightWidth��5?      $
��Left=�5@      $��23��5A      $��349,295,0,0q�5B      $��Center=�5C      $	��Top=�5E      $	��120��5F      ��5D      �� $ sliderRightWidth��$ Value�5F      �� 5H   
   -   5         ��Name$ buttonApply��5H      $��172,411,323,129q�5I      $��Apply��5J      5K      5L      5M      