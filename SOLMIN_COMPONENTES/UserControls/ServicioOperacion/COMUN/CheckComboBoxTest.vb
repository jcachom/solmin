Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Diagnostics

Namespace CheckComboBoxTest
    Public Class CheckedComboBox
        Inherits ComboBox
        ''' <summary>
        ''' Internal class to represent the dropdown list of the CheckedComboBox
        ''' </summary>
        Friend Class DD
            Inherits Form
            ' ---------------------------------- internal class CCBoxEventArgs --------------------------------------------
            ''' <summary>
            ''' Custom EventArgs encapsulating value as to whether the combo box value(s) should be assignd to or not.
            ''' </summary>
            Friend Class CCBoxEventArgs
                Inherits EventArgs
                Private m_assignValues As Boolean
                Public Property AssignValues() As Boolean
                    Get
                        Return m_assignValues
                    End Get
                    Set(ByVal value As Boolean)
                        m_assignValues = value
                    End Set
                End Property
                Private e As EventArgs
                Public Property EventArgs() As EventArgs
                    Get
                        Return e
                    End Get
                    Set(ByVal value As EventArgs)
                        e = value
                    End Set
                End Property
                Public Sub New(ByVal e As EventArgs, ByVal assignValues As Boolean)
                    MyBase.New()
                    Me.e = e
                    Me.m_assignValues = assignValues
                End Sub
            End Class

            ' ---------------------------------- internal class CustomCheckedListBox --------------------------------------------

            ''' <summary>
            ''' A custom CheckedListBox being shown within the dropdown form representing the dropdown list of the CheckedComboBox.
            ''' </summary>
            Friend Class CustomCheckedListBox
                Inherits CheckedListBox
                Private curSelIndex As Integer = -1

                Public Sub New()
                    MyBase.New()
                    Me.SelectionMode = Windows.Forms.SelectionMode.One
                    Me.HorizontalScrollbar = True
                End Sub

                ''' <summary>
                ''' Intercepts the keyboard input, [Enter] confirms a selection and [Esc] cancels it.
                ''' </summary>
                ''' <param name="e">The Key event arguments</param>
                Protected Overloads Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
                    If e.KeyCode = Keys.Enter Then
                        ' Enact selection.
                        DirectCast(Parent, CheckedComboBox.DD).OnDeactivate(New CCBoxEventArgs(Nothing, True))

                        e.Handled = True
                    ElseIf e.KeyCode = Keys.Escape Then
                        ' Cancel selection.
                        DirectCast(Parent, CheckedComboBox.DD).OnDeactivate(New CCBoxEventArgs(Nothing, False))

                        e.Handled = True
                    ElseIf e.KeyCode = Keys.Delete Then
                        ' Delete unckecks all, [Shift + Delete] checks all.
                        For i As Integer = 0 To Items.Count - 1
                            SetItemChecked(i, e.Shift)
                        Next
                        e.Handled = True
                    End If
                    ' If no Enter or Esc keys presses, let the base class handle it.
                    MyBase.OnKeyDown(e)
                End Sub

                Protected Overloads Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
                    MyBase.OnMouseMove(e)
                    Dim index As Integer = IndexFromPoint(e.Location)
                    'Debug.WriteLine("Mouse over item: " & (IIf(index >= 0, GetItemText(Items(index)), "None")))
                    If (index >= 0) AndAlso (index <> curSelIndex) Then
                        curSelIndex = index
                        SetSelected(index, True)
                    End If
                End Sub

            End Class
            ' end internal class CustomCheckedListBox
            ' --------------------------------------------------------------------------------------------------------

            ' ********************************************* Data *********************************************

            Private ccbParent_1 As CheckedComboBox

            ' Keeps track of whether checked item(s) changed, hence the value of the CheckedComboBox as a whole changed.
            ' This is simply done via maintaining the old string-representation of the value(s) and the new one and comparing them!
            Private oldStrValue As String = ""
            Public ReadOnly Property ValueChanged() As Boolean
                Get
                    Dim newStrValue As String = ccbParent_1.Text
                    If (oldStrValue.Length > 0) AndAlso (newStrValue.Length > 0) Then
                        Return (oldStrValue.CompareTo(newStrValue) <> 0)
                    Else
                        Return (oldStrValue.Length <> newStrValue.Length)
                    End If
                End Get
            End Property

            ' Array holding the checked states of the items. This will be used to reverse any changes if user cancels selection.
            Private checkedStateArr As Boolean()

            ' Whether the dropdown is closed.
            Private dropdownClosed As Boolean = True

            Private cclb As CustomCheckedListBox
            Public Property List() As CustomCheckedListBox
                Get
                    Return cclb
                End Get
                Set(ByVal value As CustomCheckedListBox)
                    cclb = value
                End Set
            End Property

            ' ********************************************* Construction *********************************************

            Public Sub New(ByVal ccbParent As CheckedComboBox)
                Me.ccbParent_1 = ccbParent
                InitializeComponent()
                Me.ShowInTaskbar = False
                ' Add a handler to notify our parent of ItemCheck events.
                AddHandler Me.cclb.ItemCheck, AddressOf Me.cclb_ItemChec
            End Sub

            ' ********************************************* Methods *********************************************

            ' Create a CustomCheckedListBox which fills up the entire form area.
            Private Sub InitializeComponent()
                Me.cclb = New CustomCheckedListBox()
                Me.SuspendLayout()
                '
                ' cclb
                '
                Me.cclb.BorderStyle = System.Windows.Forms.BorderStyle.None
                Me.cclb.Dock = System.Windows.Forms.DockStyle.Fill
                Me.cclb.FormattingEnabled = True
                Me.cclb.Location = New System.Drawing.Point(0, 0)
                Me.cclb.Name = "cclb"
                Me.cclb.Size = New System.Drawing.Size(47, 15)
                Me.cclb.TabIndex = 0
                '
                ' Dropdown
                '
                Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
                Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
                Me.BackColor = System.Drawing.SystemColors.Menu
                Me.ClientSize = New System.Drawing.Size(47, 16)
                Me.ControlBox = False
                Me.Controls.Add(Me.cclb)
                Me.ForeColor = System.Drawing.SystemColors.ControlText
                Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
                Me.MinimizeBox = False
                Me.Name = "ccbParent"
                Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
                Me.ResumeLayout(False)
            End Sub

            Public Function GetCheckedItemsStringValue() As String
                Dim sb As New StringBuilder("")
                For i As Integer = 0 To cclb.CheckedItems.Count - 1
                    sb.Append(cclb.GetItemText(cclb.CheckedItems(i))).Append(ccbParent_1.ValueSeparator)
                Next
                If sb.Length > 0 Then
                    sb.Remove(sb.Length - ccbParent_1.ValueSeparator.Length, ccbParent_1.ValueSeparator.Length)
                End If
                Return sb.ToString()
            End Function

            ''' <summary>
            ''' Closes the dropdown portion and enacts any changes according to the specified boolean parameter.
            ''' NOTE: even though the caller might ask for changes to be enacted, this doesn't necessarily mean
            ''' that any changes have occurred as such. Caller should check the ValueChanged property of the
            ''' CheckedComboBox (after the dropdown has closed) to determine any actual value changes.
            ''' </summary>
            ''' <param name="enactChanges"></param>
            Public Sub CloseDropdown(ByVal enactChanges As Boolean)
                If dropdownClosed Then
                    Exit Sub
                End If
                Debug.WriteLine("CloseDropdown")
                ' Perform the actual selection and display of checked items.
                If enactChanges Then
                    ccbParent_1.SelectedIndex = -1
                    ' Set the text portion equal to the string comprising all checked items (if any, otherwise empty!).

                    ccbParent_1.Text = GetCheckedItemsStringValue()
                Else
                    ' Caller cancelled selection - need to restore the checked items to their original state.
                    For i As Integer = 0 To cclb.Items.Count - 1
                        cclb.SetItemChecked(i, checkedStateArr(i))
                    Next
                End If
                ' From now on the dropdown is considered closed. We set the flag here to prevent OnDeactivate() calling
                ' this method once again after hiding this window.
                dropdownClosed = True
                ' Set the focus to our parent CheckedComboBox and hide the dropdown check list.
                ccbParent_1.Focus()
                Me.Hide()
                ' Notify CheckedComboBox that its dropdown is closed. (NOTE: it does not matter which parameters we pass to
                ' OnDropDownClosed() as long as the argument is CCBoxEventArgs so that the method knows the notification has
                ' come from our code and not from the framework).
                ccbParent_1.OnDropDownClosed(New CCBoxEventArgs(Nothing, False))
            End Sub

            Protected Overloads Overrides Sub OnActivated(ByVal e As EventArgs)
                Debug.WriteLine("OnActivated")
                MyBase.OnActivated(e)
                dropdownClosed = False
                ' Assign the old string value to compare with the new value for any changes.
                oldStrValue = ccbParent_1.Text
                ' Make a copy of the checked state of each item, in cace caller cancels selection.
                checkedStateArr = New Boolean(cclb.Items.Count - 1) {}
                For i As Integer = 0 To cclb.Items.Count - 1
                    checkedStateArr(i) = cclb.GetItemChecked(i)
                Next
            End Sub

            Protected Overloads Overrides Sub OnDeactivate(ByVal e As EventArgs)
                Debug.WriteLine("OnDeactivate")
                MyBase.OnDeactivate(e)
                Dim ce As CCBoxEventArgs = TryCast(e, CCBoxEventArgs)
                If ce IsNot Nothing Then
                    CloseDropdown(ce.AssignValues)
                Else
                    ' If not custom event arguments passed, means that this method was called from the
                    ' framework. We assume that the checked values should be registered regardless.
                    CloseDropdown(True)
                End If
            End Sub

            Private Sub cclb_ItemChec(ByVal sender As Object, ByVal e As ItemCheckEventArgs)

            End Sub

        End Class
        ' end internal class Dropdown
        ' ******************************** Data ********************************
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing
        ' A form-derived object representing the drop-down list of the checked combo box.
        Private dropdown As DD

        ' The valueSeparator character(s) between the ticked elements as they appear in the
        ' text portion of the CheckedComboBox.
        Private m_valueSeparator As String
        Public Property ValueSeparator() As String
            Get
                Return m_valueSeparator
            End Get
            Set(ByVal value As String)
                m_valueSeparator = value
            End Set
        End Property

        Public Property CheckOnClick() As Boolean
            Get
                Return dropdown.List.CheckOnClick
            End Get
            Set(ByVal value As Boolean)
                dropdown.List.CheckOnClick = value
            End Set
        End Property

        Private _DisplayMember As String
        Public Shadows Property DisplayMember() As String
            Get
                Return dropdown.List.DisplayMember
            End Get
            Set(ByVal value As String)
                dropdown.List.DisplayMember = "name"
                _DisplayMember = value
            End Set
        End Property


        'Private _SelectedValue As String

        'Public Shadows Property SelectedValue() As String
        '    Get
        '        Return _SelectedValue
        '    End Get
        '    Set(ByVal value As String)
        '        _SelectedValue = value
        '        For intCont As Integer = 0 To Me.Items.Count - 1
        '            If _SelectedValue = Items.Item(intCont) Then
        '                Me.Items.Item(intCont) = Items.Item(intCont)
        '                'checkedStateArr(intCont)

        '            End If
        '        Next


        '    End Set
        'End Property


        Private _DataSource As Object

        Public Shadows Property DataSource() As Object
            Get
                Return _DataSource
            End Get
            Set(ByVal value As Object)
                _DataSource = value
                Me.Items.Clear()
                Try
                    If TypeOf value Is DataTable Then
                        For i As Integer = 0 To _DataSource.Rows.Count - 1
                            Dim item As New CCBoxItem(_DataSource.Rows(i).Item(1), _DataSource.Rows(i).Item(0))
                            Me.Items.Add(item)
                        Next
                    ElseIf TypeOf value Is Object Then
                        For i As Integer = 0 To value.Count - 1
                            Dim item As New CCBoxItem(value.Item(i).GetType.GetProperty(_DisplayMember).GetValue(value.Item(i), Nothing), value.Item(i).GetType.GetProperty(Me.ValueMember).GetValue(value.Item(i), Nothing))
                            Me.Items.Add(item)
                        Next
                    End If
                Catch : End Try
            End Set
        End Property



        Public Shadows ReadOnly Property Items() As CheckedListBox.ObjectCollection
            Get
                Return dropdown.List.Items
            End Get
        End Property

        Public ReadOnly Property CheckedItems() As CheckedListBox.CheckedItemCollection
            Get
                Return dropdown.List.CheckedItems
            End Get
        End Property

        Public ReadOnly Property CheckedIndices() As CheckedListBox.CheckedIndexCollection
            Get
                Return dropdown.List.CheckedIndices
            End Get
        End Property

        Public ReadOnly Property ValueChanged() As Boolean
            Get
                Return dropdown.ValueChanged
            End Get
        End Property

        ' Event handler for when an item check state changes.
        Public Event ItemChec As ItemCheckEventHandler

        ' ******************************** Construction ********************************

        Public Sub New()
            MyBase.New()
            ' We want to do the drawing of the dropdown.
            Me.DrawMode = Windows.Forms.DrawMode.OwnerDrawVariable
            ' Default value separator.
            Me.m_valueSeparator = ", "
            ' This prevents the actual ComboBox dropdown to show, although it's not strickly-speaking necessary.
            ' But including this remove a slight flickering just before our dropdown appears (which is caused by
            ' the empty-dropdown list of the ComboBox which is displayed for fractions of a second).
            Me.DropDownHeight = 1
            ' This is the default setting - text portion is editable and user must click the arrow button
            ' to see the list portion. Although we don't want to allow the user to edit the text portion
            ' the DropDownList style is not being used because for some reason it wouldn't allow the text
            ' portion to be programmatically set. Hence we set it as editable but disable keyboard input (see below).
            Me.DropDownStyle = ComboBoxStyle.DropDown
            Me.dropdown = New DD(Me)
            ' CheckOnClick style for the dropdown (NOTE: must be set after dropdown is created).
            Me.CheckOnClick = True


        End Sub

        ' ******************************** Operations ********************************

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Protected Overloads Overrides Sub OnDropDown(ByVal e As EventArgs)
            MyBase.OnDropDown(e)
            DoDropDown()
        End Sub

        Private Sub DoDropDown()
            If Not dropdown.Visible Then
                Dim rect As Rectangle = RectangleToScreen(Me.ClientRectangle)
                dropdown.Location = New Point(rect.X, rect.Y + Me.Size.Height)
                Dim count As Integer = dropdown.List.Items.Count
                If count > Me.MaxDropDownItems Then
                    count = Me.MaxDropDownItems
                ElseIf count = 0 Then
                    count = 1
                End If
                dropdown.Size = New Size(Me.Size.Width, (dropdown.List.ItemHeight) * count + 2)
                dropdown.Show(Me)
            End If
        End Sub

        Protected Overloads Overrides Sub OnDropDownClosed(ByVal e As EventArgs)
            ' Call the handlers for this event only if the call comes from our code - NOT the framework's!
            ' NOTE: that is because the events were being fired in a wrong order, due to the actual dropdown list
            ' of the ComboBox which lies underneath our dropdown and gets involved every time.
            If TypeOf e Is DD.CCBoxEventArgs Then
                MyBase.OnDropDownClosed(e)
            End If
        End Sub

        Protected Overloads Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
            If e.KeyCode = Keys.Down Then
                ' Signal that the dropdown is "down". This is required so that the behaviour of the dropdown is the same
                ' when it is a result of user pressing the Down_Arrow (which we handle and the framework wouldn't know that
                ' the list portion is down unless we tell it so).
                ' NOTE: all that so the DropDownClosed event fires correctly!
                OnDropDown(Nothing)
            End If
            ' Make sure that certain keys or combinations are not blocked.
            e.Handled = Not e.Alt AndAlso Not (e.KeyCode = Keys.Tab) AndAlso Not ((e.KeyCode = Keys.Left) OrElse (e.KeyCode = Keys.Right) OrElse (e.KeyCode = Keys.Home) OrElse (e.KeyCode = Keys.[End]))

            MyBase.OnKeyDown(e)
        End Sub

        Protected Overloads Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)
            e.Handled = True
            MyBase.OnKeyPress(e)
        End Sub

        Public Function GetItemChecked(ByVal index As Integer) As Boolean
            If index < 0 OrElse index > Items.Count Then
                Throw New ArgumentOutOfRangeException("index", "value out of range")
            Else
                Return dropdown.List.GetItemChecked(index)
            End If
        End Function

        Public Sub SetItemChecked(ByVal index As Integer, ByVal isChecked As Boolean)
            If index < 0 OrElse index > Items.Count Then
                Throw New ArgumentOutOfRangeException("index", "value out of range")
            Else
                dropdown.List.SetItemChecked(index, isChecked)
                ' Need to update the Text.
                Me.Text = dropdown.GetCheckedItemsStringValue()
            End If
        End Sub

        Public Function GetItemCheckState(ByVal index As Integer) As CheckState
            If index < 0 OrElse index > Items.Count Then
                Throw New ArgumentOutOfRangeException("index", "value out of range")
            Else
                Return dropdown.List.GetItemCheckState(index)
            End If
        End Function

        Public Sub SetItemCheckState(ByVal index As Integer, ByVal state As CheckState)
            If index < 0 OrElse index > Items.Count Then
                Throw New ArgumentOutOfRangeException("index", "value out of range")
            Else
                dropdown.List.SetItemCheckState(index, state)
                ' Need to update the Text.
                Me.Text = dropdown.GetCheckedItemsStringValue()
            End If
        End Sub

    End Class

End Namespace