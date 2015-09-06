using HowToBBQ.Win10.Common;
using System;

namespace HowToBBQ.Win10.Models
{
    public class BBQRecipe : BindableBase
    {
        string id;
        public string Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    IsChanged = true;
                    SetProperty(ref this.id, value);
                }
            }
        }

        string name;
        public string Name {

            get { return name; }
            set
            {
                if (value != name)
                {
                    IsChanged = true;
                    SetProperty(ref this.name, value);
                }
            }

        }

        string shortDesc;
        public string ShortDesc
        {
            get { return shortDesc; }

            set
            {
                if (value !=shortDesc)
                {
                    IsChanged = true;
                    SetProperty(ref this.shortDesc, value);
                }
            }
        }


        string ingredients;
        public string Ingredients {

            get { return ingredients;  }

            set
            {
                if (value != ingredients)
                {
                    IsChanged = true;
                    SetProperty(ref this.ingredients, value);
                }
            }
        }

        string directions;
        public string Directions {

            get { return directions; }

            set
            {
                if (value != directions)
                {
                    IsChanged = true;
                    SetProperty(ref this.directions, value);
                }
            }

        }

        int prepTime;
        public int PrepTime
        {
            get { return prepTime; }

            set
            {
                if (value != prepTime)
                {
                    IsChanged = true;
                    SetProperty(ref this.prepTime, value);
                }
            }
        }

        int totalTime;
        public int TotalTime
        {
            get { return totalTime; }

            set
            {
                if (value != totalTime)
                {
                    IsChanged = true;
                    SetProperty(ref this.totalTime, value);
                }
            }
        }

        int serves;
        public int Serves
        {
            get { return serves; }

            set
            {
                if (value != serves)
                {
                    IsChanged = true;
                    SetProperty(ref this.serves, value);
                }
            }

        }

        string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (value != imagePath)
                {
                    IsChanged = true;
                    SetProperty(ref this.imagePath, value);

                    NotifyPropertyChanged("ImageUri");
                }
            }
        }
        public Uri ImageUri
        {
            get
            {
                if (!string.IsNullOrEmpty(ImagePath)) return new Uri(ImagePath, UriKind.RelativeOrAbsolute);
                else return null;
            }
        }

        string imageName;
        public string ImageName
        {
            get { return imageName; }

            set
            {
                if (value != imageName)
                {
                    IsChanged = true;
                    SetProperty(ref this.imageName, value);
                }
            }

        }

        bool isChanged = false;
        public bool IsChanged
        {
            get { return isChanged; }
            set { SetProperty(ref this.isChanged, value); }
        }
    }
}
