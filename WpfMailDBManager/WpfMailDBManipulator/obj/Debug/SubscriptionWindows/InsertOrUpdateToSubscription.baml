   M S B A M L   `   `   `  ����O  KWpfMailDBManipulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:WpfMailDBManager.SubscriptionWindows$WpfMailDBManager.SubscriptionWindows  5   	   G    AWpfMailDBManager.SubscriptionWindows.InsertOrUpdateToSubscription   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlocal2clr-namespace:WpfMailDBManager.SubscriptionWindows     5   	     � Title$J  DДобавление записи в таблицу Подписок��5   	   $	��200��66   $	��450��6C   $	��200��6O   $	��350��6_   .��5	      � .+�5
   
   � $
��Left=�6   $��	3,10,0,33q�6*   $	��119��6=   .+�5      � $��0,0,0,55q�6    � RowDefinitions 5      �� 5      6&   �� 5      6&   .+�5      �� -   5         ��Name$- 'IOUSubscription_TBl_lable_id_Subscriber��5      $��0��6M   $
��Left=�6Z   $��	10,14,0,0q�6u   $
��Wrap=�6�   $��Подписчик��6�   $	��Top=�6�   �� 5      -   5        $* $IOUSubscription_TBl_lable_id_Edition��5      $��1��6J   $
��Left=�6W   $��10,7,0,0q�6r   $
��Wrap=�6�   $��Издание��6�   $	��Top=�6�   5      �� 5      -   5        $+ %IOUSubscription_TBl_lable_date_Create��5      $
��Left=�6G   $��	10,0,0,39q�6b   $
��Wrap=�6u   $��Дата начала��6�   $��16��6�   $��Bottom=�6�   �� 5      -   5        $/ )IOUSubscription_TBl_lable_date_Expiration��5      $
��Left=�6K   $��	10,0,0,13q�6f   $
��Wrap=�6y   $��16��6�   $��Bottom=�6�   �� 6�   $�	Дата ��6�   �� 6�   $�завершения��6�   6�   5      �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��0��6T   $��127,119,0,0q�6_   $	��Top=�6t   $��7��6�   � 5   
   $��124,10,10,33q�6   .+�5      � $��0,0,0,57q�6    5      �� 5      $��* �6$   61   �� 5      $��* �6$   61   .+�5      �� -   5         ��Name$, &IOUSubscription_ComboBox_id_Subscriber��5      $��0��6L   $��
10,10,10,0q�6Y    �� Text$  ��6m   �� 5      -   5        $) #IOUSubscription_ComboBox_id_Edition��5      $��1��6H   $
��10,5q�6U   $  ��6c   5      �� 5       -   5         ��Name$&  IOUSubscription_TBox_date_Create��5       $��
10,0,10,39q�6@    �� TextWrapping$
 Wrap=�6T   $�� ��6h   ���#EEEEEE{�.  $��18��6�   $��Bottom=�6�   (  "System.Windows.Controls.DatePicker  5!      -   5          Name$( "IOUSubscription_DP_date_Expiration��5!      $��
10,0,10,10q�6E   $��24��6Y   $��Bottom=�6e   5"      � 5#   
    � ColumnDefinitions 5$      �� 5%      6%   �� 5&      6%   .+�5(      �� -	   5        	 ��Name$	 IOUSubscription_button_OK��5(      $��1��68   $��OK��6`   $��	50,0,0,15q�6m   $
��Left=�6�   $	��100��6�   $��20��6�   $��Bottom=�6�   �� 5)      -
   5        $#	 IOUSubscription_button_Cancel��5)      $��0��6<   $��Отмена��6h   $��	0,0,50,15q�6y   $��Right=�6�   $	��100��6�   $��20��6�   $��Bottom=�6�   5*      5+      5,      