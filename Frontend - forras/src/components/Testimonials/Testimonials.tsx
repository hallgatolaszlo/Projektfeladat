/* ITT DOLGOZZON A FRONTENDES FELADATRÉSZBEN! */

import { useEffect, useState } from "react";
import left from "../../assets/icons/left.svg";
import right from "../../assets/icons/right.svg";
import styles from "./Testimonials.module.css";

type TestimonialType = {
	name: string;
	age: number;
	location: string;
	testimonialText: string;
	imageUrl: string;
};

export default function Testimonials() {
	const [testimonials, setTestimonials] = useState<TestimonialType[]>([]);
	const [currentIndex, setCurrentIndex] = useState<number>(0);

	useEffect(() => {
		async function fetchData() {
			try {
				const response = await fetch("http://localhost:5202/App", {
					method: "get",
				});
				await response.json().then((d) => setTestimonials(d));
			} catch {}
		}
		fetchData();
	}, []);

	function increaseIndex() {
		setCurrentIndex(
			currentIndex == testimonials.length - 1 ? 0 : currentIndex + 1,
		);
	}

	function decreaseIndex() {
		setCurrentIndex(
			currentIndex == 0 ? testimonials.length - 1 : currentIndex - 1,
		);
	}

	return (
		<section className={styles.testimonials}>
			{testimonials.length > 0 && (
				<div className="testimonialCard">
					<div>
						<button onClick={decreaseIndex}>
							<img alt="Balra mutató nyíl" src={left} />
						</button>
						<div>
							<img
								alt="Profilkép"
								src={`/public${testimonials[currentIndex].imageUrl}`}
							/>
							<div>
								<h3>{`${testimonials[currentIndex].name} (${testimonials[currentIndex].age})`}</h3>
								<h4>{testimonials[currentIndex].location}</h4>
							</div>
							<p>{testimonials[currentIndex].testimonialText}</p>
							<a href="#">ÉN SEM HAGYOM KI!</a>
						</div>
						<button onClick={increaseIndex}>
							<img alt="Jobbra mutató nyíl" src={right} />
						</button>
					</div>
				</div>
			)}
		</section>
	);
}
