using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace WorkerScreen.ViewModel
{
    /// <summary>
    /// 뷰모델 베이스
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        /// <summary>
        /// 프로퍼티 변경 여부
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 프로퍼티 변경 이벤트 발생후 처리
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 프로퍼티 변경 이벤트 발생(Release)
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }


        /// <summary>
        /// ViewModel 인터턴스
        /// </summary>
        public static ViewModelBase Instance => _instance;
        protected static ViewModelBase _instance = null;

        /// <summary>
        /// Instance를 Clear한다.
        /// </summary>
        public static void ClearInstance() => _instance = null;

        /// <summary>
        /// 현재 인스텀스를 반한 한다.
        /// </summary>
        /// <returns></returns>
        protected abstract ViewModelBase InitObject();

        /// <summary>
        /// 모델 이름으로 모델 반환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelName">모델 이름</param>
        /// <returns></returns>
        public ObservableCollection<T> GetViewModel<T>(string modelName)
        {
            try
            {
                if (string.IsNullOrEmpty(modelName))
                    return null;

                return (ObservableCollection<T>)GetType().GetProperty(modelName).GetValue(this);
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 모델 이름으로 모델 반환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ObservableCollection<T> GetViewModel<T>()
        {
            try
            {
                return GetViewModel<T>(GetViewModelName(typeof(T)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 모델 Type으로 모델의 이름을 반환 한다.
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public string GetViewModelName(Type modelType)
        {
            var pis = GetType().GetProperties();

            foreach (var p in pis)
            {
                if (p.PropertyType.IsGenericType)
                    continue;

                if (p.PropertyType == modelType)
                    return p.Name;
            }

            return null;
        }

        
        //------------------------------------------------------------------

        /// <summary>
        /// 컬렉션 모델 이름으로 모델 반환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ObservableCollection<T> GettCollectionViewModel<T>()
        {
            try
            {
                return GetViewModel<T>(GetCollectionViewModelName(typeof(T)));
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 컬렉션 모델 Type으로 모델의 이름을 반환 한다.
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public string GetCollectionViewModelName(Type modelType)
        {
            var pis = GetType().GetProperties();

            foreach(var p in pis)
            {
                if (!p.PropertyType.IsGenericType || 
                     p.PropertyType.GetGenericTypeDefinition() != typeof(Collection<>))
                    continue;

                if (p.PropertyType.GetGenericArguments()[0] == modelType)
                    return p.Name;
            }

            return null;
        }
    }
}
