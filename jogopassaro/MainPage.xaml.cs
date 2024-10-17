﻿namespace jogopassaro
{
	public partial class MainPage : ContentPage
	{
		const int gravidade = 2;
		const int fps = 25;
		const int maxTempoPulando = 2;
		const int ForcaPulo = 35;
		const int AberturaDoCano = 5;
		bool faliceu = true;
		double LarguraJanela = 0;
		double AlturaJanela = 0;
		int xrl8 = 7;
		int TempoPulando = 20;
		bool EstaPulando = false;
		int score=0;

		public MainPage()
		{
			InitializeComponent();
		}


		void AplicaPulo()
		{
			tucano.TranslationY -= ForcaPulo;
			TempoPulando++;
			if (TempoPulando > maxTempoPulando)
			{
				EstaPulando = false;
				TempoPulando = 0;
			}
			

		}
		void Jumptucanozinho(object sender, TappedEventArgs e)
			{
				EstaPulando = true;
			}
		void OnGameOverClicked(object s, TappedEventArgs a)

		{
			FrameGameOver.IsVisible = false;
			Inicializar();
			Desenha();
		}

		void Inicializar()
		{
			faliceu = false;
			tucano.TranslationY = 0;
		}

		protected override void OnSizeAllocated(double LJ, double AJ)
		{
			base.OnSizeAllocated(LJ, AJ);
			LarguraJanela = LJ;
			AlturaJanela = AJ;
		}

		void AndarCanos()
		{
			CanoCima.TranslationX -= xrl8;
			CanoBaixo.TranslationX -= xrl8;

			if (CanoCima.TranslationX < -LarguraJanela)
			{
				CanoBaixo.TranslationX = 0;
				CanoCima.TranslationX = 0;
				var maxAltura=-100;
				var minAltura=-CanoBaixo.HeightRequest;
				CanoCima.TranslationY=Random.Shared.Next((int)minAltura, (int)maxAltura);
				CanoBaixo.TranslationY=CanoCima.TranslationY+AberturaDoCano+CanoBaixo.HeightRequest;
				score ++;
				LabelScore.Text="Canos: "+score.ToString("D3");
			}
		}


		async Task Desenha()
		{
			while (!faliceu)
			{
				{
					if (EstaPulando)
						AplicaPulo();
					else
						CriaGravidade();
				}
				CriaGravidade();
				AndarCanos();
				if (VerificaColisao())
				{
					faliceu = true;
					FrameGameOver.IsVisible = true;
					break;
				}
				await Task.Delay(fps);
			}
		}

		async void CriaGravidade()
		{
			tucano.TranslationY += gravidade;
			//tucano.TranslationY -= gravidade;



		}


		bool VerificaColisaoCima()
		{
			var minAltura = -AlturaJanela / 2;
			if (tucano.TranslationY <= minAltura)
				return true;

			else
				return false;
		}

		bool VerificaColisaoBaxo()
		{
			var maxAltura = AlturaJanela / 2;
			if (tucano.TranslationY >= maxAltura)
				return true;

			else
				return false;

		}

		bool VerificaColisaoCanoDeCima()
		{
			var minAltura = -LarguraJanela / 2;
			if (tucano.TranslationY <= minAltura)
				return true;

			else
				return false;
		}
		bool VerificaColisao()
		{
			if (!faliceu)
			{
				if (VerificaColisaoCima() || VerificaColisaoBaxo())
				{
					return true;
				}
			}
			return false;
		}


	}
}