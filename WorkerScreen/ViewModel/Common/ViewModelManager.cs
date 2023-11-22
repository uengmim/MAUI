using WorkerScreen.ViewModel;
using WorkerScreen.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WorkerScreen.ViewModels
{
    internal class ViewModelManager : ViewModelBase
    {


        /// <summary>
        /// 인스턴스를 초기화 한다,
        /// </summary>
        public static ViewModelBase InitInstance(bool isInit = false)
        {
            if (!isInit && _instance != null )
                return _instance;

            return _instance = (new ViewModelManager()).InitObject();
        }



        /// <summary>
        /// 현재 Object를 반환 한다.
        /// </summary>
        /// <returns></returns>
        protected override ViewModelBase InitObject() => this;

        //--------------------------------------------------------------------------------------

        /// <summary>
        /// 로그인 View 모델
        /// </summary>
    }
}
