using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace testMVVC.ViewModels
{
    // Cette classe servira de modèle parent à tous les futurs ViewModels de l'application
    //pour leur donner la capacité de rafraîchir l'interface graphique.
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        //déclare un événement nommé PropertyChanged qui sert à avertir
        //d'autres parties du programme (comme une interface graphique WPF)
        //qu'une propriété de la classe a été modifiée
        public event PropertyChangedEventHandler? PropertyChanged;

        // Cette méthode lève l'événement pour signaler un changement à la Vue.
        // [CallerMemberName] permet de récupérer automatiquement le nom de la propriété appelante.

        //Cette méthode permet d'écrire un simple OnPropertyChanged() n'importe où dans vos propriétés,
        //et le framework se charge de deviner quelle propriété a changé,
        //de vérifier que l'interface graphique écoute,
        //et de lui envoyer le signal de mise à jour de manière 100% sécurisée.
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Méthode utilitaire pour simplifier la mise à jour des champs privés
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
