   M S B A M L   `   `   `  ����G  CWpfMVVM.View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WpfMVVM.ViewWpfMVVM.View  5   	   H DWpfMVVM.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:WpfMVVM.Model;assembly=WpfMVVM.ModelWpfMVVM.Model 5   	        WpfMVVM.View.EditingWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WpfMVVM.View     5   	   Dmodel2clr-namespace:WpfMVVM.Model;assembly=WpfMVVM.Model     5   	   -   5        $	��300��5	   '   $	��300��64     � Title  6	   �� *WindowTitle+.��5      �� $��Center=�6   $��Center=�6.   $	��250��6I   .+�5   
   �� $��	User Name��6   �� 5   
   -   5         ��Name$ UserNameTxtBox��5   Z   $
��0 10q�6r   ��6   �� *CurrentUser.UserName+" �� ValidatesOnNotifyDataErrors . 6r   �� 5   
   $��User Password��6   �� 5   
   -   5        $ UserPassTxtBox��5   Z   $
��0 10q�6r   ��6   �� *CurrentUser.UserPass+ . 6r   �� 5   
   $��Is Admin��6   �� 5   
   -   5        $ IsAdminTxtBox��5   Y   $
��0 10q�6p   ��6   �� *CurrentUser.IsAdmin+ . 6p   �� 5   
   -   5         ��Name$ button��5   1   $
��Save��6A   $��Right=�6P   $	��Top=�6m   $��75��6�   ��6   �� *SaveCommand+6�   5      5      