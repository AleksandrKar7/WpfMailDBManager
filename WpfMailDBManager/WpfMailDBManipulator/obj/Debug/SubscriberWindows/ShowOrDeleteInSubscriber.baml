   M S B A M L   `   `   `  ����O  KWpfMailDBManipulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:WpfMailDBManager.SubscriberWindows"WpfMailDBManager.SubscriberWindows  5   	   A    ;WpfMailDBManager.SubscriberWindows.ShowOrDeleteInSubscriber   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:WpfMailDBManager.SubscriberWindows     5   	     � Title$  ShowOrDeleteInSubscriber��5   	   $	��210��6*   $	��450��67   $	��210��6C   $	��450��6S   .��5	      � .+�5
   
   � $
��Left=�6   $��	5,14,0,36q�6*   $	��119��6=   .+�5      �� -   5         ��Name$+ %SODSubscriber_TBl_lable_subscriber_Id��5      $��0��6H   $
��Left=�6U   $��10,1,0,0q�6p   $
��Wrap=�6�   $��Индекс��6�   $	��Top=�6�   �� 5      -   5        $' !SODSubscriber_TBl_lable_last_Name��5      $��0��6D   $
��Left=�6Q   $��	10,25,0,0q�6l   $
��Wrap=�6   $��Фамилия��6�   $	��Top=�6�   �� 5      -   5        $( "SODSubscriber_TBl_lable_first_Name��5      $
��Left=�6D   $��	10,49,0,0q�6_   $
��Wrap=�6r   $��Имя��6�   $��16��6�   $	��Top=�6�   �� 5      -   5        $) #SODSubscriber_TBl_lable_middle_Name��5      $
��Left=�6E   $��	10,72,0,0q�6`   $
��Wrap=�6s   $��Отчество��6�   $��16��6�   $	��Top=�6�   �� 5      -   5        $) #SODSubscriber_TBl_lable_sub_Address��5      $
��Left=�6E   $��	10,93,0,0q�6`   $
��Wrap=�6s   $��
Адрес��6�   $��16��6�   $	��Top=�6�   5      � 5   
   $��124,10,10,36q�6   .+�5      �� -   5         ��Name$&  SODSubscriber_TBox_subscriber_Id��5      $��0��6@   $��	10,5,10,0q�6M    �� TextWrapping$
 Wrap=�6`   $�� ��6t   ���#EEEEEE$��18��6�   $	��Top=�6�    �� 
IsReadOnly . 6�   �� 5      -   5        $" SODSubscriber_TBox_last_Name��5      $��0��6<   $��
10,28,10,0q�6I   $
 Wrap=�6]   $�� ��6q   ���#EEEEEE$��18��6�   $	��Top=�6�    . 6�   �� 5      -   5        $# SODSubscriber_TBox_first_Name��5      $��
10,51,10,0q�6=   $
 Wrap=�6Q   $�� ��6e   ���#EEEEEE$��18��6�   $	��Top=�6�    . 6�   �� 5      -	   5        $$ SODSubscriber_TBox_middle_Name��5      $��
10,74,10,0q�6>   $
 Wrap=�6R   $�� ��6f   ���#EEEEEE . $��18��6�   $	��Top=�6�   �� 5      -
   5        $$ SODSubscriber_TBox_sub_Address��5      $��
10,97,10,8q�6>   $
 Wrap=�6R   $�� ��6f   ���#EEEEEE . " �� VerticalScrollBarVisibility$
 Auto=�6�   5      �� 5   
   -   5         ��Name$ SODSubscriber_button_OK��5      {�. $y�Visible=�6i    �� 	IsDefault .  �� IsCancel . $��OK��6�   $��Center=�6�   $	��100��6�   $��20��6�   $��Bottom=�6�   $��171,0,171,10q�6�   � 5   
   $��0,5,0,-5q�6   	 � ColumnDefinitions	 5      �� 5      6%   �� 5      6%   .+�5      �� -   5        $% SODSubscriber_textBlock_OKInput��5      $��1��6A   $��	15,0,0,35q�6Q   {�.  $y�Visible=�6v   $
��Wrap=�6�   $D��>Для удаления записи нажмите 2 раза��6�   $��16��6�   $��Bottom=�6�   $��10(�6�   $
��Left=�6�   $	��175��6  �� 5       -   5        $" SODSubscriber_button_OKInput��5       $��1��6;   {�.  $y�Visible=�6�    . $��OK��6�   $��	50,0,0,15q�6�   $
��Left=�6�   $	��100��6�   $��20��6  $��Bottom=�6  �� 5!      -   5        $&  SODSubscriber_button_CancelInput��5!      $��0��6?   {�.  $y�Visible=�6�    . $��Отмена��6�   $��	0,0,50,15q�6�   $��Right=�6�   $	��100��6�   $��20��6  $��Bottom=�6  5"      5#      5$      