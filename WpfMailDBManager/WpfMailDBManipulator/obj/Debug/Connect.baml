   M S B A M L   `   `   `  ����O  KWpfMailDBManipulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WpfMailDBManagerWpfMailDBManager  5   	       WpfMailDBManager.Connect   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WpfMailDBManager     5   	     � Title$%  Окно подключения��5   	   $	��190��6"   $	��400��6/    � 
ResizeMode$ NoResize=�6;    � WindowStartupLocation$ CenterOwner=�6Q   .��5	      � .+�5
   
   � $
��Left=�6   $��3,10,0,0q�6*   $	��125��6<   $	��112��6H   $	��Top=�6U   .+�5      �� -   5         ��Name$" Connect_TBl_lable_ServerName��5      $��0��6>   $
��Left=�6K   $��	10,10,0,0q�6f   $
��Wrap=�6y   $��Имя сервера��6�   $	��Top=�6�   �� 5      -   5        $&  Connect_TBl_lable_Authentication��5      $
��Left=�6B   $��	10,33,0,0q�6]   $
��Wrap=�6p   $)��#Тип аутентификация��6�   $	��Top=�6�   �� 5      -   5        $  Connect_TBl_lable_UserName��5      $
��Left=�6<   $��	25,54,0,0q�6W   $
��Wrap=�6j   $��
Логин��6~   $��16��6�   $	��Top=�6�    J� RenderTransformOrigin$ 0.657,0.521&�6�   �� 5      -   5        $  Connect_TBl_lable_Password��5      $
��Left=�6<   $��	25,78,0,0q�6W   $
��Wrap=�6j   $��16��6~   $	��Top=�6�   $��Пароль��6�   5      � 5   
   $��133,10,10,0q�6   $	��112��6$   $	��Top=�61   .+�5      �� -   5         ��Name$ Connect_TBox_dataSource��5      $��
10,10,10,0q�67    �� TextWrapping$
 Wrap=�6K   $�� ��6_   ���#EEEEEE{�. $��20��6�   $	��Top=�6�   �� 5      -   5         ��Name$% Connect_ComboBox_Authentication��5       �� SelectedIndex$ 0��6@   $��
10,33,10,0q�6�   $	��Top=�6�   $��20��6�   .@�5      �� $��12(�6   Windows Authentication6B   �� 5      $��12(�6   SQL Server Authentication6E   5      �� 5      -   5        $ Connect_TBox_UserName��5      $��
25,56,10,0q�65   $
 Wrap=�6I   $�� ��6]   ���#EEEEEE{�.  $��20��6�   $	��Top=�6�   G� 5      -   5        	 G�Name$	 Connect_PassBox_Password��5      
 G� PasswordChar$
 *��6<   $��
25,79,10,0q�6M   ���#EEEEEE{�.  $��20��6�   $	��Top=�6�   5      �� 5   
   -	   5         ��Name$ Connect_button_OK��5      $��OK��6D   $��232,0,60,12q�6Q   $��20��6f   $��Bottom=�6r   �� 5   
   -
   5        $ Connect_button_Cancel��5      $��0��60   $��Отмена��6@   $��60,0,232,14q�6Q   $��20��6f   $��Bottom=�6r   6�   5      5      