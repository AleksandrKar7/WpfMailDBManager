   M S B A M L   `   `   `  ����O  KWpfMailDBManipulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:WpfMailDBManager.SubscriptionWindows$WpfMailDBManager.SubscriptionWindows  5   	   E    ?WpfMailDBManager.SubscriptionWindows.ShowOrDeleteInSubscription   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlocal2clr-namespace:WpfMailDBManager.SubscriptionWindows     5   	     � Title$H  BПросмотр записи из таблицы Подписок��5   	   $	��265��65   $	��460��6B   $	��265��6N   $	��460��6^   .��5	      � .+�5
   
   � $
��Left=�6   $��	3,10,0,40q�6*   $	��119��6=   .+�5      �� -   5         ��Name$" SODSubscription_TBl_lable_id��5      $
��Left=�6>   $��	10,10,0,0q�6Y   $
��Wrap=�6l   $��Индекс��6�   $	��Top=�6�   � 5      $��	0,31,0,65q�6    � RowDefinitions 5      �� 5      $��* �6$   61   �� 5      $��* �6$   61   .+�5      �� -   5        $- 'SODSubscription_TBl_lable_id_Subscriber��5      $��0��6M   $
��Left=�6Z   $��	10,10,0,0q�6u   $
��Wrap=�6�   $��Подписчик��6�   $	��Top=�6�   �� 5      -   5        $* $SODSubscription_TBl_lable_id_Edition��5      $��1��6J   $
��Left=�6W   $��10,8,0,0q�6r   $
��Wrap=�6�   $��Издание��6�   $��16��6�   $	��Top=�6�   5      �� 5      -   5        $+ %SODSubscription_TBl_lable_date_Create��5      $
��Left=�6G   $��	10,0,0,36q�6b   $
��Wrap=�6u   $��Дата начала��6�   $��16��6�   $��Bottom=�6�   �� 5      -   5        $/ )SODSubscription_TBl_lable_date_Expiration��5      $
��Left=�6K   $��	10,0,0,11q�6f   $
��Wrap=�6y    J� RenderTransformOrigin$ 
0.385,0.75&�6�   $��16��6�   $��Bottom=�6�   �� 6�   $�	Дата ��6�   �� 6�   $�завершения��6�   6  5      �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��0��6T   $��127,119,0,0q�6_   $	��Top=�6t   $��7��6�   � 5   
   $��124,10,10,40q�6   .+�5      � $��	0,28,0,62q�6    5      �� 5      $��* �6$   61   �� 5      $��* �6$   61   .+�5      �� -   5         ��Name$( "SODSubscription_TBox_id_Subscriber��5      $��0��6F   $��
10,10,10,0q�6S   $�� ��6g   ���#EEEEEE �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�6�   �� 5      -   5        $% SODSubscription_TBox_id_Edition��5      $��1��6C   $
��10,5q�6P   $�� ��6^   ���#EEEEEE . $
 Auto=�6�   5       �� 5!      -   5        $&  SODSubscription_TBox_date_Create��5!      $��
10,0,10,35q�6@    �� TextWrapping$
 Wrap=�6T   $�� ��6h   ���#EEEEEE$��20��6�   $��Bottom=�6�    . 6�   �� 5"      -	   5        $* $SODSubscription_Tbox_date_Expiration��5"      ���#EEEEEE$��
10,0,10,10q�6Y   $��20��6m   $��Bottom=�6y    . 6�   �� 5#      -
   5        $ SODSubscription_Tbox_Id��5#      ���#EEEEEE$��	10,8,10,0q�6L    . $��20��6q   $	��Top=�6}   5$      � 5%   
    � ColumnDefinitions 5&      �� 5'      6%   �� 5(      6%   .+�5*      �� -   5        $' !SODSubscription_textBlock_OKInput��5*      $��1��6C   $��	15,0,0,35q�6S   {�.  $y�Visible=�6x   $
��Wrap=�6�   $D��>Для удаления записи нажмите 2 раза��6�   $��16��6�   $��Bottom=�6�   $��10(�6�   $
��Left=�6   $	��175��6  �� 5+      -   5        	 ��Name$$	 SODSubscription_button_OKInput��5+      $��1��6=   {�.  $y�Visible=�6�   
 �� 	IsDefault
 . $��OK��6�   $��	50,0,0,15q�6�   $
��Left=�6�   $	��100��6�   $��20��6  $��Bottom=�6  �� 5,      -   5        $(	 "SODSubscription_button_CancelInput��5,      $��0��6A   {�.  $y�Visible=�6�    �� IsCancel . $��Отмена��6�   $��Right=�6�   $	��100��6�   $��20��6�   $��Bottom=�6�   $��	0,0,50,15q�6  5-      �� 5.   
   -   5        $	 SODSubscription_button_OK��5.      $��0��64   {�. $y�Visible=�6}   
 .  . $��OK��6�   $��176,0,176,15q�6�   $��Center=�6�   $	��100��6�   $��20��6�   $��Bottom=�6  $��2��6&  50      51      