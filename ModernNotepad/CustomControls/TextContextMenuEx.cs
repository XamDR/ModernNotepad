using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace ModernNotepad.CustomControls
{
    public class TextContextMenuEx : ContextMenu
    {
        private static readonly CommandBinding selectAllBinding;
        private static readonly CommandBinding deleteBinding;
        private static readonly CommandBinding undoBinding;
        private static readonly CommandBinding redoBinding;

        private readonly MenuItem proofingMenuItem;

        static TextContextMenuEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextContextMenuEx), new FrameworkPropertyMetadata(typeof(TextContextMenuEx)));

            selectAllBinding = new CommandBinding(ApplicationCommands.SelectAll);
            selectAllBinding.PreviewCanExecute += OnSelectAllPreviewCanExecute;

            deleteBinding = new CommandBinding(EditingCommands.Delete);
            deleteBinding.PreviewCanExecute += OnDeletePreviewCanExecute;

            undoBinding = new CommandBinding(ApplicationCommands.Undo);
            undoBinding.PreviewCanExecute += OnUndoRedoPreviewCanExecute;

            redoBinding = new CommandBinding(ApplicationCommands.Redo);
            redoBinding.PreviewCanExecute += OnUndoRedoPreviewCanExecute;
        }

        /// <summary>
        /// Initializes a new instance of the TextContextMenuEx class.
        /// </summary>
        public TextContextMenuEx()
        {
            proofingMenuItem = new MenuItem();
            Items.Add(proofingMenuItem);
            Items.Add(new MenuItem
            {
                Command = ApplicationCommands.Cut,
                Icon = new SymbolIcon(Symbol.Cut)
            });
            Items.Add(new MenuItem
            {
                Command = ApplicationCommands.Copy,
                Icon = new SymbolIcon(Symbol.Copy)
            });
            Items.Add(new MenuItem
            {
                Command = ApplicationCommands.Paste,
                Icon = new SymbolIcon(Symbol.Paste)
            });
            Items.Add(new MenuItem
            {
                Command = ApplicationCommands.Undo,
                Icon = new SymbolIcon(Symbol.Undo)
            });
            Items.Add(new MenuItem
            {
                Command = ApplicationCommands.Redo,
                Icon = new SymbolIcon(Symbol.Redo)
            });
            Items.Add(new MenuItem
            {
                Command = EditingCommands.Delete,
                Icon = new SymbolIcon(Symbol.ClearSelection)
            });
            Items.Add(new MenuItem
            {
                Command = ApplicationCommands.SelectAll,
                Icon = new SymbolIcon(Symbol.SelectAll),
                InputGestureText = "Ctrl+A"
            });            
        }

        #region UsingTextContextMenu

        public static readonly DependencyProperty UsingTextContextMenuExProperty =
            DependencyProperty.RegisterAttached(
                "UsingTextContextMenuEx",
                typeof(bool),
                typeof(TextContextMenuEx),
                new PropertyMetadata(false, OnUsingTextContextMenuExChanged));

        public static bool GetUsingTextContextMenuEx(Control textControl) 
            => (bool)textControl.GetValue(UsingTextContextMenuExProperty);

        public static void SetUsingTextContextMenuEx(Control textControl, bool value) 
            => textControl.SetValue(UsingTextContextMenuExProperty, value);

        private static void OnUsingTextContextMenuExChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textControl = (Control)d;

            if ((bool)e.NewValue)
            {
                textControl.CommandBindings.Add(selectAllBinding);
                textControl.CommandBindings.Add(deleteBinding);
                textControl.CommandBindings.Add(undoBinding);
                textControl.CommandBindings.Add(redoBinding);
                textControl.ContextMenuOpening += OnContextMenuOpening;
            }
            else
            {
                textControl.CommandBindings.Remove(selectAllBinding);
                textControl.CommandBindings.Remove(deleteBinding);
                textControl.CommandBindings.Remove(undoBinding);
                textControl.CommandBindings.Remove(redoBinding);
                textControl.ContextMenuOpening -= OnContextMenuOpening;
            }
        }

        #endregion

        protected override void OnOpened(RoutedEventArgs e)
        {
            base.OnOpened(e);
            if (proofingMenuItem.IsVisible)
            {
                proofingMenuItem.IsSubmenuOpen = true;
            }
        }

        protected override void OnClosed(RoutedEventArgs e)
        {
            base.OnClosed(e);
            if (!IsOpen)
            {
                proofingMenuItem.Items.Clear();

                foreach (MenuItem menuItem in Items)
                {
                    menuItem.ClearValue(MenuItem.CommandTargetProperty);
                }
            }
        }

        private static void OnSelectAllPreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is TextBox textBox && 
                (string.IsNullOrEmpty(textBox.Text) || textBox.SelectionLength >= textBox.Text.Length))
            {
                e.CanExecute = false;
                e.Handled = true;
            }
            else if (sender is PasswordBox passwordBox && string.IsNullOrEmpty(passwordBox.Password))
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }

        private static void OnDeletePreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrEmpty(textBox.SelectedText))
            {
                e.CanExecute = false;
                e.Handled = true;
            }
            else if (sender is PasswordBox passwordBox && string.IsNullOrEmpty(passwordBox.Password))
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }

        private static void OnUndoRedoPreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (sender is TextBoxBase textBoxBase && textBoxBase.IsReadOnly)
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }

        private static void OnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var textControl = (Control)sender;

            if (textControl.ContextMenu is TextContextMenuEx contextMenu)
            {
                Control target;

                if (textControl is PasswordBox passwordBox &&
                    PasswordBoxHelper.GetPasswordRevealMode(passwordBox) == PasswordRevealMode.Visible &&
                    e.Source is TextBox)
                {
                    target = (Control)e.Source;
                }
                else
                {
                    target = textControl;
                }
                contextMenu.UpdateItems(target);
                bool hasVisibleItems = contextMenu.Items.OfType<MenuItem>().Any(mi => mi.Visibility == Visibility.Visible);
                
                if (!hasVisibleItems)
                {
                    e.Handled = true;
                }
            }
        }

        private void UpdateProofingMenuItem(Control target)
        {            
            proofingMenuItem.Header = Application.Current.TryFindResource("Proofing");
            proofingMenuItem.Items.Clear();

            SpellingError spellingError = null;

            if (target is TextBox textBox)
            {
                spellingError = textBox.GetSpellingError(textBox.CaretIndex);
            }
            else if (target is RichTextBox richTextBox)
            {
                spellingError = richTextBox.GetSpellingError(richTextBox.CaretPosition);
            }

            if (spellingError != null)
            {
                foreach (string suggestion in spellingError.Suggestions)
                {
                    var menuItem = new MenuItem
                    {
                        Header = suggestion,
                        Command = EditingCommands.CorrectSpellingError,
                        CommandParameter = suggestion,
                        CommandTarget = target
                    };
                    proofingMenuItem.Items.Add(menuItem);
                }
                if (proofingMenuItem.HasItems)
                {
                    proofingMenuItem.Items.Add(new Separator());
                }
                proofingMenuItem.Items.Add(new MenuItem
                {                    
                    Header = Application.Current.TryFindResource("Ignore"),
                    Command = EditingCommands.IgnoreSpellingError,
                    CommandTarget = target
                });
                proofingMenuItem.Visibility = Visibility.Visible;
            }
            else
            {
                proofingMenuItem.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateItems(Control target)
        {
            UpdateProofingMenuItem(target);

            foreach (MenuItem menuItem in Items)
            {
                if (menuItem.Command is RoutedUICommand command)
                {
                    if (command == ApplicationCommands.Cut)
                    {                        
                        menuItem.Header = Application.Current.TryFindResource("Cut");
                    }
                    else if (command == ApplicationCommands.Copy)
                    {                        
                        menuItem.Header = Application.Current.TryFindResource("Copy");
                    }
                    else if (command == ApplicationCommands.Paste)
                    {                        
                        menuItem.Header = Application.Current.TryFindResource("Paste");
                    }
                    else if (command == ApplicationCommands.Undo)
                    {                        
                        menuItem.Header = Application.Current.TryFindResource("Undo");
                    }
                    else if (command == ApplicationCommands.Redo)
                    {                        
                        menuItem.Header = Application.Current.TryFindResource("Redo");
                    }
                    else if (command == EditingCommands.Delete)
                    {
                        menuItem.Header = Application.Current.TryFindResource("Delete");
                    }
                    else if (command == ApplicationCommands.SelectAll)
                    {                        
                        menuItem.Header = Application.Current.TryFindResource("SelectAll");
                    }
                    menuItem.CommandTarget = target;
                    menuItem.Visibility = command.CanExecute(null, target) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
